using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GeoSearch.API.Controllers
{
    public class GeoLocationController : BaseApiController
    {
        private readonly ILocationSearchService _locationSearchService;

        public GeoLocationController(ILocationSearchService locationSearchService)
        {
            _locationSearchService = locationSearchService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations([FromBody] LocationSearchRequest searchRequest)
        {
            var response = await _locationSearchService.GetLocationsAsync(searchRequest);
            
            return Ok(response);
        }
    }
}
