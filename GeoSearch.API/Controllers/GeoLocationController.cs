using GeoSearch.API.Filters;
using GeoSearch.API.Hubs;
using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GeoSearch.API.Controllers
{
    public class GeoLocationController : BaseApiController
    {
        private readonly IHubContext<SearchHub> _hubContext;
        private readonly ILocationService _locationService;

        public GeoLocationController(ILocationService locationService, IHubContext<SearchHub> hubContext)
        {
            _locationService = locationService;
            _hubContext = hubContext;
        }
        
        [HttpPost]
        [ServiceFilter(typeof(IdempotencyFilter))]
        public async Task<ActionResult<IEnumerable<SearchResult>>> FetchLocationsAndSaveReqAndResponse([FromBody] LocationSearchRequest searchRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid search request.");
            
            var response = await _locationService.FetchLocationsFromExternalApiAsync(searchRequest);
            var searchResult = await _locationService.SaveGeoLocationSearchWithLocations(searchRequest, response);
            
            await _hubContext.Clients.All.SendAsync("ReceiveSearchRequest", searchResult);
            
            return Ok(searchResult);
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
