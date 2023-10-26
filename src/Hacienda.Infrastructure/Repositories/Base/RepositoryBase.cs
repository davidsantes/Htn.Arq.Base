using Hacienda.Domain.Exceptions.Base;
using Hacienda.Domain.Repositories.Base;
using Hacienda.Infrastructure.DbContextEf;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hacienda.Infrastructure.Repositories.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly EntityDbContext _context;

    public RepositoryBase(EntityDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityAdded = await _context.Set<T>().AddAsync(entity);
        return entityAdded.Entity;
    }

    public async Task<T> AddAndCommitAsync(T entity)
    {
        var entityAdded = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityAdded.Entity;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<int> DeleteAndSaveAsync(int id)
    {
        T? t = _context.Set<T>().Find(id);
        if (t == null)
        {
            throw new NotFoundException($"Entidad con ID {id} no encontrada");
        }
        _context.Set<T>().Remove(t);
        return await this._context.SaveChangesAsync();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T GetById(int id)
    {
        T? t = _context.Set<T>().Find(id);
        if (t == null)
        {
            throw new NotFoundException($"Entidad con ID {id} no encontrada");
        }
        return t;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        DbSet<T> dbt = _context.Set<T>();

        T? t = await dbt.FindAsync(id);
        if (t == null)
        {
            throw new NotFoundException($"Entidad con ID {id} no encontrada");
        }
        return t;
    }

    public void Update(T entity)
    {
        this._context.Set<T>().Update(entity);
    }

    public async Task<int> UpdateAndCommitAsync(T entity)
    {
        this.Update(entity);
        return await this._context.SaveChangesAsync();
    }

    public async Task<int> CompleteAsync()
    {
        return await this._context.SaveChangesAsync();
    }
}