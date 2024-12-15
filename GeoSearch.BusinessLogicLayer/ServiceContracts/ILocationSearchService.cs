using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationSearchService
{
    Task<IEnumerable<LocationResponse>> GetLocationsAsync(LocationSearchRequest searchRequest);
}