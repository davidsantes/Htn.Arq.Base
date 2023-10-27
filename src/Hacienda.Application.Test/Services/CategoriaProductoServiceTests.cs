using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Hacienda.Application.Dtos;
using Hacienda.Application.Services;
using Hacienda.Domain.Entities;
using Hacienda.Domain.ExternalClients;
using Hacienda.Domain.Repositories;
using Hacienda.Domain.ResultErrors;
using Moq;
using Xunit;

namespace Hacienda.Application.Test.Services;

[Trait("Categoria", "CategoriaProductoService")]
public class CategoriaProductoServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfCategoriaProductoResponse()
    {
        // Arrange
        var categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        var mapperMock = new Mock<IMapper>();
        var categoriaService = new CategoriaProductoService(categoriaRepositoryMock.Object, null, mapperMock.Object, null, null);

        var categorias = new List<CategoriaProducto>(); // Agrega algunas categorías de ejemplo
        var categoriaProductoResponses = new List<GetCategoriaProductoResponse>(); // Agrega respuestas de ejemplo

        categoriaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categorias);
        mapperMock.Setup(mapper => mapper.Map<List<GetCategoriaProductoResponse>>(categorias)).Returns(categoriaProductoResponses);

        // Act
        var result = await categoriaService.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(categoriaProductoResponses);
    }

    [Fact]
    public async Task GetAsync_WhenCategoriaExists_ShouldReturnCategoriaProductoResponse()
    {
        // Arrange
        var categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        var mapperMock = new Mock<IMapper>();
        var categoriaService = new CategoriaProductoService(categoriaRepositoryMock.Object, null, mapperMock.Object, null, null);

        var categoriaId = 1; // ID de una categoría existente
        var categoria = CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion");
        var categoriaProductoResponse = new GetCategoriaProductoResponse();

        categoriaRepositoryMock.Setup(repo => repo.GetByIdAsync(categoriaId)).ReturnsAsync(categoria);
        mapperMock.Setup(mapper => mapper.Map<GetCategoriaProductoResponse>(categoria)).Returns(categoriaProductoResponse);

        // Act
        var result = await categoriaService.GetAsync(categoriaId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(categoriaProductoResponse);
    }

    [Fact]
    public async Task InsAsync_WhenInsertSucceeds_ShouldCallRepositoryAndCorreosAdapter()
    {
        // Arrange
        var categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        var correosAdapterMock = new Mock<ICorreosClientAdapter>();
        var mapperMock = new Mock<IMapper>();
        var validatorInsertCategoriaMock = new Mock<IValidator<InsertCategoriaProductoRequest>>();
        var categoriaService = new CategoriaProductoService(
            categoriaRepositoryMock.Object,
            correosAdapterMock.Object,
            mapperMock.Object,
            validatorInsertCategoriaMock.Object,
            null);

        var insertCategoriaRequest = new InsertCategoriaProductoRequest(nombre: "Nombre", descripcion: "Descripcion");

        // Simula la inserción exitosa y devuelve una categoría con ID
        categoriaRepositoryMock.Setup(repo => repo.AddAndCommitAsync(It.IsAny<CategoriaProducto>()))
            .ReturnsAsync(CategoriaProducto.Crear(nombre: "Nombre", descripcion: "Descripcion"));

        // Simula un envío de correo exitoso
        correosAdapterMock.Setup(adapter => adapter.InsAsync()).ReturnsAsync(new Result<bool>(true));

        // Act
        var result = await categoriaService.InsAsync(insertCategoriaRequest);

        // Assert

        // Verifica que se llamó al método AddAndCommitAsync en el repositorio una vez con una instancia de CategoriaProducto
        categoriaRepositoryMock.Verify(repo => repo.AddAndCommitAsync(It.IsAny<CategoriaProducto>()), Times.Once);

        // Verifica que se llamó al método InsAsync en el adaptador de correos una vez
        correosAdapterMock.Verify(adapter => adapter.InsAsync(), Times.Once);
    }
}