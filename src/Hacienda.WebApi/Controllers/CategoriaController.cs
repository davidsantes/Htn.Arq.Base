using Hacienda.Application.Dtos;
using Hacienda.Application.ProblemDetails;
using Hacienda.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hacienda.WebApi.Controllers;

[ApiController]
[Route(RouteBaseWithVersion)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaProductoService _categoriaService;
    public readonly IProblemDetailsFactory _problemDetailsFactory;
    private const string RouteBaseWithVersion = "api/v1/[controller]";

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
    [HttpGet("getCategorias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<GetCategoriaProductoResponse>>> GetCategorias()
    {
        var categorias = await _categoriaService.GetAllAsync();
        if (!categorias.Any())
        {
            return NotFound(_problemDetailsFactory.GetResourceNotFound());
        }

        return Ok(categorias);
    }

    /// <summary>
    /// Retorna una categoría de producto concreta (dato maestro).
    /// </summary>
    /// <returns>Categoría buscada</returns>
    [HttpGet("{id}", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> InsCategoria(InsertCategoriaProductoRequest nuevaCategoriaRequest)
    {
        var insCategoriaResult = await _categoriaService.InsAsync(nuevaCategoriaRequest);

        if (insCategoriaResult.IsSuccess)
        {
            var actionName = nameof(Get);
            var routeValues = new { id = insCategoriaResult.Value};
            return CreatedAtAction(actionName, routeValues, nuevaCategoriaRequest);
        }
        else
        {
            var problemaEnInsercion = _problemDetailsFactory.GetBackendProblem(insCategoriaResult.Errors);
            return BadRequest(problemaEnInsercion);
        }
    }

    /// <summary>
    /// Actualiza una categoría de producto concreta.
    /// </summary>
    /// <returns>Si el resultado ha sido satisfactorio</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]   
    public async Task<IActionResult> UpdCategoria([FromRoute] int id, [FromBody] UpdateCategoriaProductoRequest categoriaRequest)
    {
        categoriaRequest.Id = id;
        var updCategoriaResult = await _categoriaService.UpdAsync(categoriaRequest);

        if (updCategoriaResult.IsSuccess)
        {
            return Ok(new { id = categoriaRequest.Id });
        }
        else if (updCategoriaResult.Errors.ContainsKey("CategoriaNoEncontrada"))
        {
            return NotFound(_problemDetailsFactory.GetResourceNotFound());
        }
        else
        {
            var problemaEnActualizacion = _problemDetailsFactory.GetBackendProblem(updCategoriaResult.Errors);
            return BadRequest(problemaEnActualizacion);
        }
    }

    /// <summary>
    /// Elimina una categoría de producto concreta.
    /// </summary>
    /// <returns>Si el resultado ha sido satisfactorio</returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DelCategoria(int id)
    {
        var delCategoriaResult = await _categoriaService.DelAsync(id);

        if (delCategoriaResult.IsSuccess)
        {
            return NoContent();
        }
        else if (delCategoriaResult.Errors.ContainsKey("CategoriaNoEncontrada"))
        {
            return NotFound(_problemDetailsFactory.GetResourceNotFound());
        }
        else
        {
            var problemaEnEliminacion = _problemDetailsFactory.GetBackendProblem(delCategoriaResult.Errors);
            return BadRequest(problemaEnEliminacion);
        }
    }
}