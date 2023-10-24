using Hacienda.Application.Dtos;
using Hacienda.Application.ProblemDetails;
using Hacienda.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hacienda.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaProductoService _categoriaService;
    public readonly IProblemDetailsFactory _problemDetailsFactory;

    public CategoriaController(ICategoriaProductoService categoriaService,
        IProblemDetailsFactory problemDetailsFactory)
    {
        _categoriaService = categoriaService;
        _problemDetailsFactory = problemDetailsFactory;
    }

    /// <summary>
    /// Retorna las categorías de productos (dato maestro).
    /// </summary>
    /// <returns>Listado de categorías</returns>
    [HttpGet("getCategoriasProducto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<GetCategoriaProductoResponse>>> GetCategoriasProducto()
    {
        var categorias = await _categoriaService.GetAllAsync();
        if (!categorias.Any())
        {
            return NotFound(_problemDetailsFactory.CreateRecursoNoEncontrado());
        }

        return Ok(categorias);
    }

    /// <summary>
    /// Retorna una categoría de producto concreta (dato maestro).
    /// </summary>
    /// <returns>Categoría buscada</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("Categoria/{id}")]
    public async Task<ActionResult<GetCategoriaProductoResponse>> Get(int id)
    {
        var categoria = await _categoriaService.GetAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    /// <summary>
    /// Inserta una categoría de producto concreta.
    /// </summary>
    /// <returns>Si el resultado ha sido satisfactorio</returns>
    [HttpPost("insCategoriaProducto")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InsCategoriaProducto(InsertCategoriaProductoRequest nuevaCategoriaRequest)
    {
        var insCategoriaResult = await _categoriaService.InsAsync(nuevaCategoriaRequest);

        if (insCategoriaResult.IsSuccess)
        {
            return CreatedAtAction(nameof(Get)
                , new { id = insCategoriaResult.Value }
                , nuevaCategoriaRequest);
        }
        else
        {
            var problemaEnInsercion = _problemDetailsFactory
                .CreateProblemaEnBackEnd(insCategoriaResult.Errors);
            return BadRequest(problemaEnInsercion);
        }
    }
}