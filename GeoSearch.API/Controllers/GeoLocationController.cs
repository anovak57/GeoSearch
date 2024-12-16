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
        public async Task<ActionResult<IEnumerable<LocationResponse>>> FetchLocationsAndSaveReqAndResponse([FromBody] LocationSearchRequest? searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid search request.");
            }
            
            var response = await _locationService.FetchLocationsFromExternalApiAsync(searchRequest);
            
            await _locationService.SaveLocations(response);
            await _locationService.SaveLocationSearchRequest(searchRequest);
            
            return Ok(response);
        }

        [HttpGet("geolocations")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations()
        {
            var locations = await _locationService.FetchLocationsFromDatabase();
            return Ok(locations);
        }
        
        [HttpGet("location-searches")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocationSearches()
        {
            var locationSearches = await _locationService.FetchLocationSearches();
            return Ok(locationSearches);
        }
    }
}
