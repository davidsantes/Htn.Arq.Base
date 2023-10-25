using Hacienda.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hacienda.Infrastructure.DbContextEf;

public class EntityDbContext : DbContext
{
    public EntityDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EntityDbContextCategoriaConfiguration());
    }
}