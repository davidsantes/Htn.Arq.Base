using Hacienda.Domain.Entities;
using Hacienda.Domain.Results;

namespace Hacienda.Domain.Repositories;

public interface ICategoriaRepositoryPruebaDapper
{
    /// <summary>
    /// Retorna todas las categorías
    /// </summary>
    /// <returns>Listado de categorías</returns>
    Task<IList<Categoria>> GetAllAsync();

    /// <summary>
    /// Retorna una categoría en concreto
    /// </summary>
    /// <param name="id">Identificador de la categoría</param>
    /// <returns>Categoría concreta</returns>
    Task<Categoria> GetAsync(Guid id);

    /// <summary>
    /// Inserta una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a insertar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultToReturnWithObject<Guid>> InsAsync(Categoria categoria);

    /// <summary>
    /// Actualiza una categoría en concreto
    /// </summary>
    /// <param name="categoria">Categoría a actualizar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultToReturnWithObject<int>> UpdAsync(Categoria categoria);

    /// <summary>
    /// Elimina una categoría en concreto
    /// </summary>
    /// <param name="id">Categoría a eliminar</param>
    /// <returns>Registros afectados, mensajes de si todo ha ido correcto o no</returns>
    Task<ResultToReturnWithObject<int>> DelAsync(Guid id);
}