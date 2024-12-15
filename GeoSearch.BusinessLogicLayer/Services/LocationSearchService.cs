using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.Services;

public class LocationSearchService : ILocationSearchService
{
    private readonly ILocationApiWrapper _locationApiWrapper;

    public LocationSearchService(ILocationApiWrapper locationApiWrapper)
    {
       _locationApiWrapper = locationApiWrapper; 
    }

    public async Task<IEnumerable<LocationResponse>> GetLocationsAsync(LocationSearchRequest searchRequest)
    {
        IEnumerable<GeoLocation> locations = await _locationApiWrapper.SearchPlacesAsync(searchRequest);
        IEnumerable<LocationResponse> locationResponses = locations.Select(loc => loc.ToLocationResponse());
        
        return locationResponses;
    }
}