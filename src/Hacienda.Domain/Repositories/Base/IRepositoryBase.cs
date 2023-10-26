using System.Linq.Expressions;

namespace Hacienda.Domain.Repositories.Base;

public interface IRepositoryBase<T> where T : class
{
    /// <summary>
    /// Retorna una entidad, a raíz de un identificador concreto. La ejecución se realizará de manera síncrona
    /// </summary>
    /// <param name="id">Identificador a buscar</param>
    /// <returns>Entidad encontrada</returns>
    T GetById(int id);

    /// <summary>
    /// Retorna una entidad, a raíz de un identificador concreto. La ejecución se realizará de manera asíncrona
    /// </summary>
    /// <param name="id">Identificador a buscar</param>
    /// <returns>Entidad encontrada</returns>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// Retorna todas las entidades de un tipo
    /// </summary>
    /// <returns>Lista de entidades La ejecución se realizará de manera asíncrona</returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Busca y recupera elementos de tipo T que satisfacen una condición específica definida por una expresión.
    /// </summary>
    /// <param name="expression">La expresión que define la condición de búsqueda.</param>
    /// <returns>Una colección de elementos que cumplen con la condición especificada.</returns>
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Agrega una entidad de tipo T de manera asíncrona. No persistirá en base de datos hasta que se haga un commit.
    /// </summary>
    /// <param name="entity">La entidad a agregar.</param>
    /// <returns>La entidad agregada.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Agrega una entidad de tipo T de manera asíncrona y persiste los cambios en el contexto.
    /// </summary>
    /// <param name="entity">La entidad a agregar.</param>
    /// <returns>La entidad agregada.</returns>
    Task<T> AddAndCommitAsync(T entity);

    /// <summary>
    /// Actualiza una entidad de tipo T en el contexto.
    /// </summary>
    /// <param name="entity">La entidad a actualizar.</param>
    void Update(T entity);

    /// <summary>
    /// Actualiza una entidad de tipo T en el contexto y persiste los cambios de manera asíncrona.
    /// </summary>
    /// <param name="entity">La entidad a actualizar.</param>
    /// <returns>El número de entidades actualizadas en el contexto.</returns>
    Task<int> UpdateAndCommitAsync(T entity);

    /// <summary>
    /// Elimina una entidad de tipo T del contexto.
    /// </summary>
    /// <param name="entity">La entidad a eliminar.</param>
    void Delete(T entity);

    /// <summary>
    /// Elimina una entidad de tipo T a raíz de su identificador y persiste los cambios en el contexto de manera asíncrona.
    /// </summary>
    /// <param name="id">Identificador de la entidad a eliminar.</param>
    /// <returns>El número de entidades eliminadas en el contexto.</returns>
    Task<int> DeleteAndSaveAsync(int id);

    /// <summary>
    /// Persiste todos los cambios en el contexto de manera asíncrona.
    /// </summary>
    /// <returns>El número de entidades afectadas por los cambios en el contexto.</returns>
    Task<int> CompleteAsync();
}