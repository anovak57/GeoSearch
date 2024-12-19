using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.DataAccessLayer.Entities;
using GeoSearch.DataAccessLayer.RepositoryContracts;

namespace GeoSearch.BusinessLogicLayer.Services;

public class LocationService : ILocationService
{
    private readonly ILocationApiWrapper _locationApiWrapper;
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationApiWrapper locationApiWrapper, ILocationRepository locationRepository)
    {
       _locationApiWrapper = locationApiWrapper; 
       _locationRepository = locationRepository;
    }

    public async Task<IEnumerable<LocationResponse>> FetchLocationsFromExternalApiAsync(LocationSearchRequest searchRequest)
    {
        IEnumerable<GeoLocation> locations = await _locationApiWrapper.SearchPlacesAsync(searchRequest);
        IEnumerable<LocationResponse> locationResponses = locations.Select(loc => loc.MapToLocationResponse());
        
        return locationResponses;
    }

    public async Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabase()
    {
        var locations = await _locationRepository.GetGeoLocations();
        
        var locationResponses = locations.Select(loc => loc.MapToLocationResponse());
        
        return locationResponses;
    }

    public async Task<IEnumerable<LocationSearchResponse>> FetchLocationSearches()
    {
        var locationSearches = await _locationRepository.GetLocationSearches();
        
        var locationSearchResponses = locationSearches.Select(loc => loc.MapToLocationSearchResponse());
        
        return locationSearchResponses;
    }
    
    public async Task<SearchResult> SaveGeoLocationSearchWithLocations(
        LocationSearchRequest searchRequest, IEnumerable<LocationResponse> locationResponses)
    {
        GeoLocationSearch geoLocationSearch = searchRequest.MapToGeoLocationSearch();

        List<GeoLocation> geoLocations = locationResponses
            .Select(loc => loc.MapToGeoLocation())
            .ToList();

        var savedSearch = await _locationRepository.AddGeoLocationSearchWithLocations(geoLocationSearch, geoLocations);
        
        var searchResult = savedSearch.MapToSearchResult();

        return searchResult;
    }

    public async Task<IEnumerable<LocationResponse>> GetLocationByCategory(string query)
    {
        var locations = await _locationRepository.GetGeoLocationsByCategory(query);
        
        var locationResponses = locations.Select(loc => loc.MapToLocationResponse());
        
        return locationResponses;
    }

    public async Task AddFavouriteLocation(int userId, int locationId)
    {
        await _locationRepository.AddFavouriteLocation(userId, locationId);
    }

    public async Task<IEnumerable<LocationResponse>> GetFavouriteLocations(int userId)
    {
        var locations = await _locationRepository.GetFavouriteLocations(userId);
        
        var locationResponses = locations.Select(loc => loc.MapToLocationResponse());
        
        return locationResponses;
    }
}