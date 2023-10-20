﻿using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Hacienda.Application.Adapters;
using Hacienda.Application.Dtos;
using Hacienda.Application.Services;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Moq;
using Xunit;

namespace Hacienda.Application.Test.Services
{
    [Trait("Categoria", "CategoriaProductoService")]
    public class CategoriaProductoServiceTests
    {
        [Fact]
        public async Task Dado_CategoriaProductoService_CuandoConsigoTodosValores_EntoncesElMapeoEsOk()
        {
            // Arrange
            var categoriaRepository = new Mock<ICategoriaRepository>();
            var mapper = new Mock<IMapper>();
            var service = new CategoriaProductoService(categoriaRepository.Object, null, mapper.Object, null);

            var categoriasEnRepositorio = new List<CategoriaProducto>
            {
                new CategoriaProducto { Id = 1, Nombre = "Electrónica", Descripcion = "Desc" },
                new CategoriaProducto { Id = 2, Nombre = "Ropa", Descripcion = "Desc"  },
                new CategoriaProducto { Id = 3, Nombre = "Hogar", Descripcion = "Desc"  },
            };
            var mappedResponse = new List<GetCategoriaProductoResponse>
            {
                new GetCategoriaProductoResponse { Id = "1", Nombre = "Electrónica" },
                new GetCategoriaProductoResponse { Id = "2", Nombre = "Ropa"},
                new GetCategoriaProductoResponse { Id = "3", Nombre = "Hogar"},
            };

            categoriaRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categoriasEnRepositorio);
            mapper.Setup(m => m.Map<List<GetCategoriaProductoResponse>>(categoriasEnRepositorio)).Returns(mappedResponse);

            // Act
            var result = await service.GetCategoriasProductoAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mappedResponse); // Verifica que las categorías sean equivalentes
        }

        [Fact]
        public async Task Dado_CategoriaProductoService_CuandoInsertoNueva_EntoncesLlamaRepositorioYCorreoOk()
        {
            // Arrange
            var categoriaRepository = new Mock<ICategoriaRepository>();
            var correosAdapter = new Mock<ICorreosAdapter>();
            var mapper = new Mock<IMapper>();
            var validator = new Mock<IValidator<InsertCategoriaProductoRequest>>();
            var service = new CategoriaProductoService(categoriaRepository.Object
                , correosAdapter.Object
                , mapper.Object
                , validator.Object);

            var nuevaCategoriaRequest = new InsertCategoriaProductoRequest { Id = "1", Nombre = "Nueva Categoría" };

            var resultadoCategoriaRepository = new Result<int>(1);
            categoriaRepository.Setup(repo => repo.InsAsync(It.IsAny<CategoriaProducto>()))
                .ReturnsAsync(resultadoCategoriaRepository);
            
            var resultadoCorreosAdapter = new Result<bool>(true);
            correosAdapter.Setup(ca => ca.InsAsync())
                .ReturnsAsync(resultadoCorreosAdapter);

            // Act
            var result = await service.InsCategoriaProductoAsync(nuevaCategoriaRequest);
            
            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(int.Parse(nuevaCategoriaRequest.Id));
            categoriaRepository.Verify(repo => repo.InsAsync(It.IsAny<CategoriaProducto>()), Times.Once);
            correosAdapter.Verify(ca => ca.InsAsync(), Times.Once);
        }
    }
}