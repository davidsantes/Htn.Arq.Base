using FluentAssertions;
using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Dal.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Htn.Arq.Base.Bll.Services.Test
{
    [Trait("Categoria", "CategoriaProductoService")]
    public class CategoriaProductoServiceTests
    {
        [Fact]
        public async Task Dado_CategoriaProductoService_CuandoConsigoTodosValores_EntoncesOk()
        {
            // Arrange
            var mockRepository = new Mock<ICategoriaRepository>();
            var expectedCategories = new List<CategoriaProducto>
            {
                new CategoriaProducto { Id = 1, Nombre = "Electrónica" },
                new CategoriaProducto { Id = 2, Nombre = "Ropa" },
                new CategoriaProducto { Id = 3, Nombre = "Hogar" },
            };

            mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedCategories);
            var service = new CategoriaProductoService(mockRepository.Object);

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
            var newCategoria = new CategoriaProducto { Id = 0, Nombre = "Nueva Categoría" };
            var expectedNewId = 4; // El siguiente ID esperado
            var expectedResult = new Result<int>(expectedNewId);


            mockRepository.Setup(repo => repo.InsAsync(newCategoria))
                .ReturnsAsync(expectedResult);
            var service = new CategoriaProductoService(mockRepository.Object);

            // Act
            var insResult = await service.InsCategoriaProductoAsync(newCategoria);

            // Assert
            insResult.Value.Should().Be(expectedNewId); // Verifica que el ID retornado sea el esperado
        }
    }
}