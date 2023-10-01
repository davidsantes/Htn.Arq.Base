using AutoMapper;
using FluentValidation;
using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.WebApi.Dtos;
using Htn.Infrastructure.Global.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Htn.Arq.Base.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaProductoService _categoriaService;
        private readonly IValidator<CategoriaProductoDto> _validator;
        public readonly IMapper _mapper;

        public CategoriaController(ICategoriaProductoService categoriaService,
            IValidator<CategoriaProductoDto> validator,
            IMapper mapper,
            ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _validator = validator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna las categorías de productos (dato maestro).
        /// </summary>
        /// <returns>Listado de categorías</returns>
        [HttpGet("getCategoriasProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<CategoriaProductoDto>>> GetCategoriasProducto()
        {
            var categorias = await _categoriaService.GetCategoriasProductoAsync();
            if (!categorias.Any())
            {
                var problemaEnBusqueda = new ProblemDetails
                {
                    Title = Global_Resources.MsgRecursoNoEncontrado,
                    Detail = Global_Resources.MsgRecursoNoEncontrado,
                    Status = StatusCodes.Status404NotFound
                };

                return NotFound(problemaEnBusqueda);
            }

            var listaCategoriasDto = _mapper.Map<List<CategoriaProductoDto>>(categorias);

            return Ok(listaCategoriasDto);
        }

        /// <summary>
        /// Inserta una categoría de producto concreta.
        /// </summary>
        /// <returns>Si el resultado ha sido satisfactorio</returns>
        [HttpPost("insCategoriaProducto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsCategoriaProducto(CategoriaProductoDto nuevaCategoriaDto)
        {
            _validator.ValidateAndThrow(nuevaCategoriaDto);

            var _mappedCategoria = _mapper.Map<CategoriaProducto>(nuevaCategoriaDto);

            var insCategoriaResult = await _categoriaService.InsCategoriaProductoAsync(_mappedCategoria);

            if (insCategoriaResult.IsSuccess)
            {
                return CreatedAtAction(nameof(InsCategoriaProducto)
                        , new { id = insCategoriaResult.Value }
                        , nuevaCategoriaDto);
            }
            else
            {
                var problemaEnInsercion = new ProblemDetails
                {
                    Title = Global_Resources.MsgOperacionKoTitulo,
                    Detail = Global_Resources.MsgOperacionKo + string.Join(",", insCategoriaResult.Errors),
                    Status = StatusCodes.Status400BadRequest
                };
                return BadRequest(problemaEnInsercion);
            }
        }
    }
}