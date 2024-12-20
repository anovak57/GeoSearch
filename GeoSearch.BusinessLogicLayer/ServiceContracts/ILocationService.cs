using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationService
{
    Task<IEnumerable<LocationResponse>> FetchLocationsFromExternalApiAsync(LocationSearchRequest searchRequest);
    Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabaseAsync();
    Task<IEnumerable<LocationSearchResponse>> FetchLocationSearchesAsync();
    Task<SearchResult> SaveGeoLocationSearchWithLocationsAsync(LocationSearchRequest searchRequest, IEnumerable<LocationResponse> locationResponses);
    Task<IEnumerable<LocationResponse>> GetLocationByCategoryAsync(string query);
    Task AddFavouriteLocationAsync(int userId, int locationId);
    Task<IEnumerable<LocationResponse>> GetFavouriteLocationsAsync(int userId);
}