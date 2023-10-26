namespace Hacienda.Domain.UnitOfWork;

/// <summary>
/// Unit of work para persistencia de datos con repositorios.
/// </summary>
public interface IUnitOfWork
{
    Task<int> CompleteAsync();

    int Complete();

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}