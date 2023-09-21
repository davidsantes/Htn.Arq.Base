using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Mvc;


namespace Htn.Arq.Base.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getVersion")]
        public IActionResult GetVersion()
        {
            string version = Global_Resources.version;
            return Ok(version);
        }

        [HttpPost("log")]
        public IActionResult LogMessage()
        {
            string version = Global_Resources.version;
            var guidControl = Guid.NewGuid();

            //TODO: mejorar sistema de logs para que lo deje estructurado: https://www.milanjovanovic.tech/blog/structured-logging-in-asp-net-core-with-serilog
            _logger.LogDebug("LogDebug. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());

            //_logger.LogDebug("Este es un mensaje de LogDebug. Versión de producto: {@version}", version);
            //_logger.LogInformation("Este es un mensaje LogInformation. Versión de producto: {@version}", version);
            //_logger.LogWarning("Este es un mensaje de LogWarning. Versión de producto: {@version}", version);
            //_logger.LogError("Este es un mensaje de LogError. Versión de producto: {@version}", version);
            //_logger.LogCritical("Este es un mensaje de LogCritical. Versión de producto: {@version}", version);
            return Ok("Mensajes de registro ejecutado.");
        }
    }
}