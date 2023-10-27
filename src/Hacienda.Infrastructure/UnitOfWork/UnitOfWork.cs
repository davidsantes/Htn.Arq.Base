using Hacienda.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Hacienda.Infrastructure.UnitOfWork;

/// <summary>
/// Implementación sacada de:
/// https://github.com/kgrzybek/modular-monolith-with-ddd/
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}