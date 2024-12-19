using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationService
{
    Task<IEnumerable<LocationResponse>> FetchLocationsFromExternalApiAsync(LocationSearchRequest searchRequest);
    Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabase();
    Task<IEnumerable<LocationSearchResponse>> FetchLocationSearches();
    Task<SearchResult> SaveGeoLocationSearchWithLocations(LocationSearchRequest searchRequest,
        IEnumerable<LocationResponse> locationResponses);
    Task<IEnumerable<LocationResponse>> GetLocationByCategory(string query);
    Task AddFavouriteLocation(int userId, int locationId);
    Task<IEnumerable<LocationResponse>> GetFavouriteLocations(int userId);
}