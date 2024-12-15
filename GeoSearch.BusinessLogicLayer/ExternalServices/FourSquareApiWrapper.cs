using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GeoSearch.BusinessLogicLayer.ExternalServices;

public class FourSquareApiWrapper : ILocationApiWrapper
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public FourSquareApiWrapper(IConfiguration config, HttpClient httpClient)
    {
        _baseUrl = config["Foursquare:BaseUrl"]!;
        _apiKey = config["Foursquare:ApiKey"]!;
        _httpClient = httpClient;
        
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _apiKey);
    }
    
    public async Task<IEnumerable<GeoLocation>> SearchPlacesAsync(LocationSearchRequest searchRequest)
    {
        var request = BuildRequest(searchRequest);

        using var response = await _httpClient.SendAsync(request);
        
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"An error occurred while trying to reach Foursquare API. Status Code: {(int)response.StatusCode}.");
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        var foursquareResult = JsonConvert.DeserializeObject<FoursquareResponse>(responseContent)
                               ?? throw new InvalidOperationException("Unexpected null response from Foursquare API.");
            
        return foursquareResult.Results.Select(result => result.ToGeoLocation());
    }

    private HttpRequestMessage BuildRequest(LocationSearchRequest searchRequest)
    {
        return new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = BuildUri(searchRequest),
        };
    }

    private Uri BuildUri(LocationSearchRequest searchRequest)
    {
        return new Uri($"{_baseUrl}" +
                       $"?query={Uri.EscapeDataString(searchRequest.Query)}" +
                       $"&ll={searchRequest.Latitude},{searchRequest.Longitude}" +
                       $"&radius={searchRequest.Radius}");
    }
}