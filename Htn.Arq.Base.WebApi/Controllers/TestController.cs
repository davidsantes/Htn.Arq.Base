using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Verifica la versión del producto.
        /// </summary>
        /// <returns>Versión</returns>
        [HttpGet("getVersion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVersion()
        {
            string version = Global_Resources.VersionApp;
            return Ok(version);
        }

        /// <summary>
        /// Inserta logs de LogDebug, LogInformation, LogWarning, LogError y LogCritical.
        /// Permite verificar si se escribe en los proveedores (sink) especificados en la aplicación.
        /// </summary>
        /// <returns>Mensaje informativo (no relevante)</returns>
        [HttpPost("insLog")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult InsLog()
        {
            string version = Global_Resources.VersionApp;
            var guidControl = Guid.NewGuid();

            _logger.LogDebug("LogDebug. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogInformation("LogInformation. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogWarning("LogWarning. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogError("LogError. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            _logger.LogCritical("LogCritical. Versión de producto {@version} - {@guidControl}", version, guidControl.ToString());
            return Ok("Mensajes de registro ejecutado.");
        }

        /// <summary>
        /// Provoca una excepción. Sirve para verificar el control de excepciones.
        /// </summary>
        /// <returns>Mensaje de error</returns>
        [HttpGet("insExcepcion")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult InsExcepcion()
        {
            var operadorIncorrecto = 0;
            // Realizar una operación que podría generar una excepción no controlada
            // Esto generará una excepción de división por cero

#pragma warning disable S1481
            int resultado = 10 / operadorIncorrecto; // Esto generará una excepción de división por cero
#pragma warning restore S1481

            return StatusCode(StatusCodes.Status500InternalServerError
                , new { Message = Global_Resources.MsgExcepcionNoControlada });
        }
    }
}