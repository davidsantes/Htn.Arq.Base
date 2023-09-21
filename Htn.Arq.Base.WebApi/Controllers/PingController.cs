using Microsoft.AspNetCore.Mvc;
using Htn.Infrastructure.Global.Resources;

namespace Htn.Arq.Base.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVersion()
        {
            string version = Global_Resources.version;         
            return Ok(version);
        }
    }
}