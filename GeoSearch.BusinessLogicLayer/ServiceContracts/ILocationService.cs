using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationService
{
    Task<IEnumerable<LocationResponse>> FetchLocationsFromExternalApiAsync(LocationSearchRequest searchRequest);
    Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabase();
    Task<IEnumerable<LocationSearchResponse>> FetchLocationSearches();
    Task<IEnumerable<LocationResponse>> SaveLocations(IEnumerable<LocationResponse> locations);
    Task<LocationSearchRequest> SaveLocationSearchRequest(LocationSearchRequest searchRequest);
    Task<IEnumerable<LocationResponse>> GetLocationByCategory(string query);
    Task AddFavouriteLocation(int userId, int locationId);
    Task<IEnumerable<LocationResponse>> GetFavouriteLocations(int userId);
}