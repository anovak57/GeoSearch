using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GeoSearch.API.Controllers
{
    public class GeoLocationController : BaseApiController
    {
        private readonly ILocationService _locationService;

        public GeoLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        
        [HttpPost]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> NewLocationSearch([FromBody] LocationSearchRequest searchRequest)
        {
            var response = await _locationService.GetLocationsAsync(searchRequest);
            
            return Ok(response);
        }
    }
}
