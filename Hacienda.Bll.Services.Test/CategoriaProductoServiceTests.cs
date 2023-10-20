using FluentAssertions;
using Hacienda.Bll.Entities;
using Hacienda.Bll.Services.Services;
using Hacienda.Dal.Interfaces.Adapters;
using Hacienda.Dal.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Hacienda.Bll.Services.Test
{
    [Trait("Categoria", "CategoriaProductoService")]
    public class CategoriaProductoServiceTests
    {
        [Fact]
        public async Task Dado_CategoriaProductoService_CuandoConsigoTodosValores_EntoncesOk()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var mockCorreosAdapter = new Mock<ICorreosAdapter>();

            var expectedCategories = new List<CategoriaProducto>
            {
                new CategoriaProducto { Id = 1, Nombre = "Electrónica" },
                new CategoriaProducto { Id = 2, Nombre = "Ropa" },
                new CategoriaProducto { Id = 3, Nombre = "Hogar" },
            };

            mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedCategories);
            mockCorreosAdapter.Setup(adapter => adapter.InsAsync())
                .ReturnsAsync(new Result<bool>(true));

            var service = new CategoriaProductoService(mockRepository.Object
                , mockCorreosAdapter.Object);

            // Act
            var result = await service.GetCategoriasProductoAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedCategories); // Verifica que las categorías sean equivalentes
        }

        [Fact]
        public async Task Dado_CategoriaProductoService_CuandoInsertoNueva_EntoncesOk()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var mockCorreosAdapter = new Mock<ICorreosAdapter>();

            var newCategoria = new CategoriaProducto { Id = 0, Nombre = "Nueva Categoría" };
            var expectedNewId = 4; // El siguiente ID esperado
            var expectedResult = new Result<int>(expectedNewId);

            mockRepository.Setup(repo => repo.InsAsync(newCategoria))
                .ReturnsAsync(expectedResult);
            mockCorreosAdapter.Setup(adapter => adapter.InsAsync())
                .ReturnsAsync(new Result<bool>(true));

            var service = new CategoriaProductoService(mockRepository.Object
                , mockCorreosAdapter.Object);

            // Act
            var insResult = await service.InsCategoriaProductoAsync(newCategoria);

            // Assert
            insResult.Value.Should().Be(expectedNewId); // Verifica que el ID retornado sea el esperado
        }
    }
}