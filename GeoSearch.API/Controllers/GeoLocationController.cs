using GeoSearch.API.Filters;
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
        [ServiceFilter(typeof(IdempotencyFilter))]
        public async Task<ActionResult<SearchResult>> FetchLocationsAndSaveReqAndResponse([FromBody] LocationSearchRequest searchRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid search request.");
            
            var locationResponses = await _locationService.FetchLocationsFromExternalApiAsync(searchRequest);

            var searchResult = await _locationService.SaveGeoLocationSearchWithLocationsAsync(searchRequest, locationResponses);

            return Ok(searchResult);
        }

        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocations()
        {
            var locations = await _locationService.FetchLocationsFromDatabaseAsync();
            return Ok(locations);
        }
        
        [HttpGet("location-searches")]
        public async Task<ActionResult<IEnumerable<LocationSearchResponse>>> GetLocationSearches()
        {
            var locationSearches = await _locationService.FetchLocationSearchesAsync();
            return Ok(locationSearches);
        }
        
        [HttpGet("locations-by-category/{query}")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetLocationsByCategory(string query)
        {
            var locations = await _locationService.GetLocationByCategoryAsync(query);
            return Ok(locations);
        }

        [HttpPost("user/{userId}/locations/{locationId}/save-favourite")]
        public async Task<ActionResult> AddLocationFavourite(int userId, int locationId)
        {
            await _locationService.AddFavouriteLocationAsync(userId, locationId);
            return NoContent();
        }

        [HttpGet("user/{userId}/favourite-locations")]
        public async Task<ActionResult<IEnumerable<LocationResponse>>> GetFavouriteLocationsByUser(int userId)
        {
            var locations = await _locationService.GetFavouriteLocationsAsync(userId);
            return Ok(locations);
        }
    }
}
