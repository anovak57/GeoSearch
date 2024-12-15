using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace GeoSearch.BusinessLogicLayer.ExternalServices;

public class FourSquareApiWrapper : ILocationApiWrapper
{
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public FourSquareApiWrapper(IConfiguration config)
    {
        _baseUrl = config["Foursquare:BaseUrl"]!;
        _apiKey = config["Foursquare:ApiKey"]!;
    }
    
    public async Task<IEnumerable<GeoLocation>> SearchPlacesAsync(LocationSearchRequest searchRequest)
    {
        var options = new RestClientOptions(
            $"{_baseUrl}" +
            $"?query={searchRequest.Query}" +
            $"&ll={searchRequest.Latitude},{searchRequest.Longitude}" +
            $"&radius={searchRequest.Radius}");
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", _apiKey);
        
        var response = await client.GetAsync(request);
        var foursquareResult = JsonConvert.DeserializeObject<FoursquareResponse>(response.Content);

        return foursquareResult.Results.Select(result => result.ToGeoLocation());
    }
}