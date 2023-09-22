using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Htn.Arq.Base.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getVersion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVersion()
        {
            string version = Global_Resources.version;
            return Ok(version);
        }

        [HttpPost("logMessage")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LogMessage()
        {
            string version = Global_Resources.version;
            var guidControl = Guid.NewGuid();
           
            _logger.LogDebug("LogDebug. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogInformation("LogInformation. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogWarning("LogWarning. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogError("LogError. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogCritical("LogCritical. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            return Ok("Mensajes de registro ejecutado.");
        }

        [HttpGet("throwUncontrolledException")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ThrowUncontrolledException()
        {
            var operadorIncorrecto = 0;
            // Realizar una operación que podría generar una excepción no controlada
            int resultado = 10 / operadorIncorrecto; // Esto generará una excepción de división por cero
            
            return StatusCode(StatusCodes.Status500InternalServerError
                , new { Message = "Se produjo una excepción no controlada." });
        }
    }
}