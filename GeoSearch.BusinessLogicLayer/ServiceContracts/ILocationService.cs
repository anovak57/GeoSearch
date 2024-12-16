using GeoSearch.BusinessLogicLayer.DTO;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationService
{
    Task<IEnumerable<LocationResponse>> FetchLocationsFromExternalApiAsync(LocationSearchRequest searchRequest);
    Task<IEnumerable<LocationResponse>> FetchLocationsFromDatabase();
    Task<IEnumerable<LocationSearchResponse>> FetchLocationSearches();
    Task<IEnumerable<LocationResponse>> SaveLocations(IEnumerable<LocationResponse> locations);
    Task<LocationSearchRequest> SaveLocationSearchRequest(LocationSearchRequest searchRequest);
}