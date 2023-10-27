using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;

namespace Hacienda.Application.Services;

public interface ICategoriaProductoService
{
    /// <summary>
    /// Retorna todas las categorías
    /// </summary>
    /// <returns>Listado de categorías</returns>
    Task<IList<GetCategoriaProductoResponse>> GetAllAsync();

    /// <summary>
    /// Retorna una categoría en concreto
    /// </summary>
    /// <param name="id">Identificador de la categoría</param>
    /// <returns>Categoría concreta</returns>
    Task<GetCategoriaProductoResponse> GetAsync(Guid id);

    /// <summary>
    /// Inserta una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a insertar</param>
    /// <returns>Identificador de la categoría, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultRequest<Guid>> InsAsync(InsertCategoriaProductoRequest categoria);

    /// <summary>
    /// Actualiza una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a actualizar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultRequest<int>> UpdAsync(UpdateCategoriaProductoRequest categoria);

    /// <summary>
    /// Elimina una categoría en concreto
    /// </summary>
    /// <param name="id">Categoría a eliminar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultRequest<int>> DelAsync(Guid id);
}