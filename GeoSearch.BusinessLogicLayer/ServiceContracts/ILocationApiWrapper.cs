using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.ServiceContracts;

public interface ILocationApiWrapper
{
    Task<IEnumerable<GeoLocation>> SearchPlacesAsync(LocationSearchRequest searchRequest);
}