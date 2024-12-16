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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations()
        {
            var locations = await _locationService.FetchLocationsFromDatabase();
            return Ok(locations);
        }
        
        [HttpGet("location-searches")]
        public async Task<ActionResult<IEnumerable<LocationSearchResponse>>> GetLocationSearches()
        {
            var locationSearches = await _locationService.FetchLocationSearches();
            return Ok(locationSearches);
        }
        
        [HttpGet("{query}")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocationsByCategory(string query)
        {
            var locations = await _locationService.GetLocationByCategory(query);
            return Ok(locations);
        }

        [HttpPost("user/{userId}/locations/{locationId}/save-favourite")]
        public async Task<ActionResult> AddLocationFavourite(int userId, int locationId)
        {
            await _locationService.AddFavouriteLocation(userId, locationId);
            
            return NoContent();
        }

        [HttpGet("user/{userId}/favourite-locations")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetFavouriteLocationsByUser(int userId)
        {
            var locations = await _locationService.GetFavouriteLocations(userId);
            
            return Ok(locations);
        }
    }
}
