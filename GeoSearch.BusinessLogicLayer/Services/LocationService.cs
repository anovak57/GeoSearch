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
        var locations = await _locationApiWrapper.SearchPlacesAsync(searchRequest);
        return locations.Select(loc => loc.MapToLocationResponse());
    }

    public async Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabaseAsync()
    {
        var locations = await _locationRepository.GetGeoLocationsAsync();
        return locations.Select(loc => loc.MapToLocationResponse());
    }

    public async Task<IEnumerable<LocationSearchResponse>> FetchLocationSearchesAsync()
    {
        var locationSearches = await _locationRepository.GetLocationSearchesAsync();
        return locationSearches.Select(search => search.MapToLocationSearchResponse());
    }

    public async Task<SearchResult> SaveGeoLocationSearchWithLocationsAsync(
        LocationSearchRequest searchRequest, IEnumerable<LocationResponse> locationResponses)
    {
        var geoLocationSearch = searchRequest.MapToGeoLocationSearch();
        var geoLocations = locationResponses.Select(loc => loc.MapToGeoLocation()).ToList();

        await HandleCategoriesAndGeoLocationsAsync(geoLocations);

        geoLocationSearch.GeoLocations = geoLocations;

        await _locationRepository.AddGeoLocationSearchAsync(geoLocationSearch);
        await _locationRepository.SaveChangesAsync();

        return geoLocationSearch.MapToSearchResult();
    }

    private async Task HandleCategoriesAndGeoLocationsAsync(List<GeoLocation> geoLocations)
    {
        var categoryNames = geoLocations
            .SelectMany(gl => gl.LocationCategories)
            .Select(lc => lc.Category.Name)
            .Distinct(StringComparer.OrdinalIgnoreCase) 
            .ToList();

        var existingCategories = await _locationRepository.GetExistingCategoriesAsync(categoryNames);

        var newCategories = categoryNames
            .Except(existingCategories.Keys, StringComparer.OrdinalIgnoreCase) 
            .Select(name => new Category { Name = name })
            .ToList();

        if (newCategories.Any())
        {
            await _locationRepository.AddCategoriesAsync(newCategories);
            await _locationRepository.SaveChangesAsync();
        }

        var allCategories = existingCategories.Values.Concat(newCategories).ToList();

        foreach (var geoLocation in geoLocations)
        {
            geoLocation.LocationCategories = geoLocation.LocationCategories
                .Select(lc => new LocationCategory
                {
                    GeoLocation = geoLocation,
                    Category = allCategories.First(c => string.Equals(c.Name, lc.Category.Name, StringComparison.OrdinalIgnoreCase))
                })
                .ToList();
        }

        await _locationRepository.AddGeoLocationsAsync(geoLocations);
        await _locationRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<LocationResponse>> GetLocationByCategoryAsync(string query)
    {
        var locations = await _locationRepository.GetGeoLocationsByCategoryAsync(query);
        return locations.Select(loc => loc.MapToLocationResponse());
    }

    public async Task AddFavouriteLocationAsync(int userId, int locationId)
    {
        if (await _locationRepository.IsFavouriteLocationAsync(userId, locationId))
        {
            throw new InvalidOperationException("This location is already marked as a favorite.");
        }

        var favouriteLocation = new FavouriteLocation
        {
            UserId = userId,
            LocationId = locationId
        };

        await _locationRepository.AddFavouriteLocationAsync(favouriteLocation);
        await _locationRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<LocationResponse>> GetFavouriteLocationsAsync(int userId)
    {
        var locations = await _locationRepository.GetFavouriteLocationsAsync(userId);
        return locations.Select(loc => loc.MapToLocationResponse());
    }
}
