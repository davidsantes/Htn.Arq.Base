using FluentAssertions;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Exceptions.Base;
using Hacienda.Infrastructure.DbContextEf;
using Hacienda.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xunit;

namespace Hacienda.Infrastructure.Test.Repositories;

[Trait("Repositories", "Base")]
public class RepositoryBaseTests
{
    [Fact]
    public void GetById_WhenEntityFound_ShouldReturnEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityId = 1;

        // Inserta datos in-memory database
        dbContext.Add(new CategoriaProducto { Id = 1, Nombre = "Nombre", Descripcion = "Descripcion" });
        dbContext.SaveChanges();

        // Act
        var result = repository.GetById(entityId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entityId);
    }

    [Fact]
    public void GetById_WhenEntityNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityIdNotExists = 999;

        // Act & Assert
        Action action = () => repository.GetById(entityIdNotExists);
        action.Should().Throw<NotFoundException>();
    }

    [Fact]
    public async Task GetByIdAsync_WhenEntityFound_ShouldReturnEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityId = 1;

        // Inserta datos in-memory database
        dbContext.Add(new CategoriaProducto { Id = 1, Nombre = "Categoría 1" });
        dbContext.SaveChanges();

        // Act
        var result = await repository.GetByIdAsync(entityId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entityId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenEntityNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityIdNotExists = 999;

        // Act & Assert
        Func<Task> action = async () => await repository.GetByIdAsync(entityIdNotExists);
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);

        // Inserta datos in-memory database
        dbContext.Add(new CategoriaProducto { Id = 1, Nombre = "Categoría 1" });
        dbContext.Add(new CategoriaProducto { Id = 2, Nombre = "Categoría 2" });
        dbContext.SaveChanges();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task FindPagedAsync_WithValidData_ShouldReturnPaginatedResult()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);

        // Inserción de datos on the fly:
        var testData = new List<CategoriaProducto>
            {
                new CategoriaProducto { Id = 1, Nombre = "Categoría si cumple con filtro" },
                new CategoriaProducto { Id = 2, Nombre = "Categoría si cumple con filtro" },
                new CategoriaProducto { Id = 3, Nombre = "Categoría si cumple con filtro" },
                new CategoriaProducto { Id = 4, Nombre = "Categoría no cumple con filtro" },
                new CategoriaProducto { Id = 5, Nombre = "Categoría no cumple con filtro" },
            };

        await dbContext.CategoriaProductos.AddRangeAsync(testData);
        await dbContext.SaveChangesAsync();

        Expression<Func<CategoriaProducto, bool>> expression = category => category.Nombre.Contains("Categoría si cumple con filtro");
        int currentPage = 2;
        int pageSize = 2;

        // Act
        var result = await repository.FindPagedAsync(expression, currentPage, pageSize);

        // Assert
        // En total habrá 3 elementos que cumplen el filtro, pero en la página 2 solo habrá 1 elemento, but only 1 item matches the filter
        result.Items.Should().HaveCount(1);
        result.CurrentPage.Should().Be(currentPage);
        result.PageSize.Should().Be(pageSize);
        result.TotalItems.Should().Be(3);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var newEntity = new CategoriaProducto { Nombre = "Nueva Categoría" };

        // Act
        var result = await repository.AddAsync(newEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);
    }

    [Fact]
    public async Task Delete_WhenEntityExists_ShouldDeleteEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityId = 1;

        // Inserta datos in-memory database
        dbContext.Add(new CategoriaProducto { Id = 1, Nombre = "Categoría 1" });
        dbContext.SaveChanges();

        // Act
        await  repository.DeleteAndSaveAsync(entityId);

        // Assert
        dbContext.CategoriaProductos.Should().NotContain(e => e.Id == entityId);
    }

    [Fact]
    public async Task AddAndCommitAsync_ShouldAddAndCommitEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var newEntity = new CategoriaProducto { Nombre = "Nueva Categoría" };

        // Act
        var result = await repository.AddAndCommitAsync(newEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);

        // Verifica que la entidad se haya guardado correctamente en la base de datos
        var storedEntity = dbContext.CategoriaProductos.FirstOrDefault(e => e.Id == result.Id);
        storedEntity.Should().NotBeNull();
        storedEntity.Nombre.Should().Be("Nueva Categoría");
    }

    [Fact]
    public async Task UpdateAndCommitAsync_ShouldUpdateAndCommitEntity()
    {
        // Arrange
        var dbContextOptions = new DbContextOptionsBuilder<EntityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new EntityDbContext(dbContextOptions);
        var repository = new RepositoryBase<CategoriaProducto>(dbContext);
        var entityToUpdate = new CategoriaProducto { Nombre = "Categoría a Actualizar" };

        dbContext.Add(entityToUpdate);
        dbContext.SaveChanges();

        // Modifica la entidad
        entityToUpdate.Nombre = "Categoría Actualizada";

        // Act
        var result = await repository.UpdateAndCommitAsync(entityToUpdate);

        // Assert
        result.Should().NotBe(0);

        // Verifica que la entidad se haya actualizado correctamente en la base de datos
        var updatedEntity = dbContext.CategoriaProductos.FirstOrDefault(e => e.Id == entityToUpdate.Id);
        updatedEntity.Should().NotBeNull();
        updatedEntity.Nombre.Should().Be("Categoría Actualizada");
    }

}