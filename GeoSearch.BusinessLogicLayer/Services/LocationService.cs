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
        IEnumerable<LocationResponse> locationResponses = locations.Select(loc => loc.ToLocationResponse());
        
        return locationResponses;
    }

    public async Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabase()
    {
        var locations = await _locationRepository.GetGeoLocations();
        
        var locationResponses = locations.Select(loc => loc.ToLocationResponse());
        
        return locationResponses;
    }

    public async Task<IEnumerable<LocationSearchResponse>> FetchLocationSearches()
    {
        var locationSearches = await _locationRepository.GetLocationSearches();
        
        var locationSearchResponses = locationSearches.Select(loc => loc.ToLocationSearchResponse());
        
        return locationSearchResponses;
    }

    public async Task<IEnumerable<LocationResponse>> SaveLocations(IEnumerable<LocationResponse> locations)
    {
        var locationResponses = locations.ToList();
        
        IEnumerable<GeoLocation> geoLocations = locationResponses.Select(loc => loc.ToGeoLocation());
        await _locationRepository.AddGeoLocations(geoLocations);

        return locationResponses;
    }

    public async Task<LocationSearchRequest> SaveLocationSearchRequest(LocationSearchRequest searchRequest)
    {
        GeoLocationSearch geoLocationSearch = searchRequest.ToGeoLocationSearch();
        await _locationRepository.AddLocationSearch(geoLocationSearch);
        
        return searchRequest;
    }

    public async Task<IEnumerable<LocationResponse>> GetLocationByCategory(string query)
    {
        var locations = await _locationRepository.GetGeoLocationsByCategory(query);
        
        var locationResponses = locations.Select(loc => loc.ToLocationResponse());
        
        return locationResponses;
    }

    public async Task AddFavouriteLocation(int userId, int locationId)
    {
        await _locationRepository.AddFavouriteLocation(userId, locationId);
    }

    public async Task<IEnumerable<LocationResponse>> GetFavouriteLocations(int userId)
    {
        var locations = await _locationRepository.GetFavouriteLocations(userId);
        
        var locationResponses = locations.Select(loc => loc.ToLocationResponse());
        
        return locationResponses;
    }
}