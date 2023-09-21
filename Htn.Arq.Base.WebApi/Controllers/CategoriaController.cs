using AutoMapper;
using FluentValidation;
using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.WebApi.Dtos;
using Htn.Infrastructure.Core.Exceptions.Entities;
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
            IMapper mapper)
        {
            _categoriaService = categoriaService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<CategoriaProductoDto>>> GetCategoriasProducto()
        {
            //TODO: omitir estas líneas que provocan error
            // a custom app exception that will return a 500 response
            //throw new CustomException("Email or password is incorrect");

            var categorias = await _categoriaService.GetCategoriasProductoAsync();
            if (!categorias.Any())
            {
                //TODO: ponerlo con recursos
                return NotFound(new Error() { Codigo = "404", Descripcion = "No encontrado" });
            }

            var listaCategoriasDto = _mapper.Map<List<CategoriaProductoDto>>(categorias);

            return Ok(listaCategoriasDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsCategoriaProducto(CategoriaProductoDto nuevaCategoriaDto)
        {
            var result = await _validator.ValidateAsync(nuevaCategoriaDto);
            if (!result.IsValid)
            {
                return BadRequest("Objeto no válido: " + string.Join(",", result.Errors));
            }

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
                return BadRequest("Objeto no válido: " + string.Join(",", insCategoriaResult.Errors));
            }
        }
    }
}