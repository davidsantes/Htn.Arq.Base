using System.Linq.Expressions;

namespace Hacienda.Domain.Repositories.Base;

public interface IRepositoryBase<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    T GetById(int id);

    IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Añade una nueva entidad del tipo especificado en la implementación. No se persiste hasta que se realice un context.Complete()
    /// </summary>
    Task<T> AddAsync(T entity);

    Task<T> AddAndCommitAsync(T entity);

    void Update(T entity);

    /// <summary>
    /// Actualiza una nueva entidad del tipo especificado en la implementación. Persiste sin necesidad de realizar un context.Complete()
    /// </summary>
    Task<int> UpdateAndCommitAsync(T entity);

    void Delete(T entity);

    Task<int> DeleteAndSaveAsync(int id);

    Task<int> CompleteAsync();
}