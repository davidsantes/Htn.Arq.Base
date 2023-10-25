using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Hacienda.Domain.ResultErrors;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Hacienda.Infrastructure.Repositories;

public class CategoriaRepository : RepositoryBase<CategoriaProducto>, ICategoriaRepository
{
    public CategoriaRepository(EntityDbContext context): base(context)
    {

    }

    /// <inheritdoc />
    public async Task<IList<CategoriaProducto>> GetAllAsync()
    {
        var categorias = await _context.CategoriaProductos.AsNoTracking().ToListAsync();
        return categorias;
    }

    /// <inheritdoc />
    public async Task<CategoriaProducto> GetAsync(int id)
    {
        var categoria = await _context.CategoriaProductos.FindAsync(id);
        return categoria;
    }

    /// <inheritdoc />
    public async Task<Result<int>> InsAsync(CategoriaProducto categoria)
    {
        _context.CategoriaProductos.Add(categoria);
        await _context.SaveChangesAsync();
        var result = new Result<int>(categoria.Id);

        return result;
    }

    /// <inheritdoc />
    public async Task<Result<int>> UpdAsync(CategoriaProducto categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        int affectedRows = await _context.SaveChangesAsync();

        var result = new Result<int>(affectedRows);
        if (affectedRows == 0)
        {
            var (Key, Message) = ResultMessageFactory.CreateMessage("CategoriaNoEncontrada");
            result.AddErrorMessage(Key, Message);
        }
        return result;
    }


    /// <inheritdoc />
    public async Task<Result<int>> DelAsync(int id)
    {
        var categoria = await _context.CategoriaProductos.FindAsync(id);
        int affectedRows = 0;
        if (categoria != null)
        {
            _context.CategoriaProductos.Remove(categoria);
            affectedRows = await _context.SaveChangesAsync();
        }
        var result = new Result<int>(affectedRows);
        if (affectedRows == 0)
        {
            var (Key, Message) = ResultMessageFactory.CreateMessage("CategoriaNoEncontrada");
            result.AddErrorMessage(Key, Message);
        }
        return result;
    }
}