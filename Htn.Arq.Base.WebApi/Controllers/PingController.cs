using Microsoft.AspNetCore.Mvc;

namespace Htn.Arq.Base.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVersion()
        {
            //TODO: poner que acceda a un recurso de global.resources
            string version = "1.0";
            return Ok(version);
        }
    }
}