using Hacienda.Domain.Entities;
using Hacienda.Domain.ResultErrors;

namespace Hacienda.Domain.Repositories;

public interface ICategoriaRepository
{
    /// <summary>
    /// Retorna todas las categorías
    /// </summary>
    /// <returns>Listado de categorías</returns>
    Task<IList<CategoriaProducto>> GetAllAsync();

    /// <summary>
    /// Retorna una categoría en concreto
    /// </summary>
    /// <param name="id">Identificador de la categoría</param>
    /// <returns>Categoría concreta</returns>
    Task<CategoriaProducto> GetAsync(int id);

    /// <summary>
    /// Inserta una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a insertar</param>
    /// <returns>Identificador de la categoría, mensajes de si todo ha ido correcto o no</returns>
    Task<Result<int>> InsAsync(CategoriaProducto categoria);

    /// <summary>
    /// Actualiza una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a actualizar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<Result<int>> UpdAsync(CategoriaProducto categoria);

    /// <summary>
    /// Elimina una categoría en concreto
    /// </summary>
    /// <param name="id">Categoría a eliminar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<Result<int>> DelAsync(int id);
}