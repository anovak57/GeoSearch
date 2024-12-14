using Microsoft.AspNetCore.Mvc;

namespace GeoSearch.API.Controllers
{
    public class GeoLocationController : BaseApiController
    {
        [HttpGet]
        public  ActionResult TestEndpoint()
        {
            return Ok();
        }
    }
}
