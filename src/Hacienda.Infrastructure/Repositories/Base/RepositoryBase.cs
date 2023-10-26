using Hacienda.Domain.Exceptions.Base;
using Hacienda.Domain.Repositories.Base;
using Hacienda.Domain.Results;
using Hacienda.Infrastructure.DbContextEf;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hacienda.Infrastructure.Repositories.Base;

/// <inheritdoc />
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly EntityDbContext _context;

    public RepositoryBase(EntityDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        T? t = _context.Set<T>().Find(id);
        if (t == null)
        {
            throw new NotFoundException(entityType: typeof(T), entityId: id);
        }
        return t;
    }

    /// <inheritdoc />
    public async Task<T> GetByIdAsync(int id)
    {
        DbSet<T> dbt = _context.Set<T>();

        T? t = await dbt.FindAsync(id);
        if (t == null)
        {
            throw new NotFoundException(entityType: typeof(T), entityId: id);
        }
        return t;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    /// <inheritdoc />
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    /// <inheritdoc />
    public async Task<PaginatedResult<T>> FindPagedAsync(Expression<Func<T, bool>> expression, int page, int pageSize)
    {
        IQueryable<T> query = _context.Set<T>().Where(expression);
        int totalItems = await query.CountAsync();
        List<T> items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<T>(items, page, pageSize, totalItems);
    }

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity)
    {
        var entityAdded = await _context.Set<T>().AddAsync(entity);
        return entityAdded.Entity;
    }

    /// <inheritdoc />
    public async Task<T> AddAndCommitAsync(T entity)
    {
        var entityAdded = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityAdded.Entity;
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    /// <inheritdoc />
    public async Task<int> UpdateAndCommitAsync(T entity)
    {
        Update(entity);
        return await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    /// <inheritdoc />
    public async Task<int> DeleteAndSaveAsync(int id)
    {
        T? t = _context.Set<T>().Find(id);
        if (t == null)
        {
            throw new NotFoundException(entityType: typeof(T), entityId: id);
        }
        _context.Set<T>().Remove(t);
        return await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}