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
        var nuevaCategoria = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");
        dbContext.Add(nuevaCategoria);
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
        var nuevaCategoria = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");
        dbContext.Add(nuevaCategoria);
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
        var nuevaCategoria1 = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");
        var nuevaCategoria2 = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");
        dbContext.Add(nuevaCategoria1);
        dbContext.Add(nuevaCategoria2);
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

        var descriptionExpected = "Categoría si cumple con filtro";
        var descriptionNonExpected = "Categoría no cumple con filtro";

        // Inserción de datos on the fly:
        var testData = new List<CategoriaProducto>
        {
            CategoriaProducto.Crear(nombre: "Nombre", descripcion: descriptionExpected),
            CategoriaProducto.Crear(nombre: "Nombre", descripcion: descriptionExpected),
            CategoriaProducto.Crear(nombre: "Nombre", descripcion: descriptionExpected),
            CategoriaProducto.Crear(nombre: "Nombre", descripcion: descriptionNonExpected)
        };

        await dbContext.CategoriaProductos.AddRangeAsync(testData);
        await dbContext.SaveChangesAsync();

        Expression<Func<CategoriaProducto, bool>> expression = category => category.Descripcion.Contains(descriptionExpected);
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
        var newEntity = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");

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
        dbContext.Add(CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion"));
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
        var nameExpected = "Nueva Categoría";
        var newEntity = CategoriaProducto.Crear(nombre: nameExpected, descripcion: "Descripcion");

        // Act
        var result = await repository.AddAndCommitAsync(newEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);

        // Verifica que la entidad se haya guardado correctamente en la base de datos
        var storedEntity = dbContext.CategoriaProductos.FirstOrDefault(e => e.Id == result.Id);
        storedEntity.Should().NotBeNull();
        storedEntity.Nombre.Should().Be(nameExpected);
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
        var entityToUpdate = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");

        dbContext.Add(entityToUpdate);
        dbContext.SaveChanges();

        // Modifica la entidad
        var nuevoNombre = "Nombre Actualizado";
        entityToUpdate.CambiarNombre(nuevoNombre);

        // Act
        var result = await repository.UpdateAndCommitAsync(entityToUpdate);

        // Assert
        result.Should().NotBe(0);

        // Verifica que la entidad se haya actualizado correctamente en la base de datos
        var updatedEntity = dbContext.CategoriaProductos.FirstOrDefault(e => e.Id == entityToUpdate.Id);
        updatedEntity.Should().NotBeNull();
        updatedEntity.Nombre.Should().Be(nuevoNombre);
    }
}