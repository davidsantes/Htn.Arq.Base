using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.Repositories.Base;

namespace Hacienda.Infrastructure.Repositories;

public class CategoriaRepository : RepositoryBase<CategoriaProducto>, ICategoriaRepository
{
    public CategoriaRepository(EntityDbContext context) : base(context)
    {
    }
}