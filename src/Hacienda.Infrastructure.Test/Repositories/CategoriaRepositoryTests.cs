using FluentAssertions;
using Hacienda.Domain.Entities;
using Hacienda.Infrastructure.Repositories;
using Xunit;

namespace Hacienda.Infrastructure.Test.Repositories;

[Trait("Categoria", "CategoriaRepository")]
public class CategoriaRepositoryTests
{
    [Fact]
    public async Task Dado_CategoriaRepository_CuandoConsigoTodosValores_EntoncesOk()
    {
        // Arrange
        var repository = new CategoriaRepository();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterOrEqualTo(3);
        result.Should().Contain(c => c.Nombre == "Electrónica");
        result.Should().Contain(c => c.Nombre == "Ropa");
        result.Should().Contain(c => c.Nombre == "Hogar");
    }

    [Fact]
    public async Task Dado_CategoriaRepository_CuandoInsertoNueva_EntoncesOk()
    {
        // Arrange
        var repository = new CategoriaRepository();
        var newCategoria = new CategoriaProducto() { Id=1, Nombre = "Juguetes" };

        // Act
        var allCategories = await repository.GetAllAsync();
        var insResult = await repository.InsAsync(newCategoria);

        // Assert
        insResult.Value.Should().BeGreaterThan(0);
        allCategories.Should().Contain(c => c.Nombre == "Juguetes"); // La nueva categoría debe estar presente
    }
}