﻿using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using Hacienda.Domain.Results;

namespace Hacienda.Domain.Test.Entities
{
    [Trait("Result", "Errors")]
    public class ResultTests
    {
        [Fact]
        public void Dado_Result_CuandoConstructorCorrecto_EntoncesOk()
        {
            // Arrange
            var idMoq = 1;

            // Act
            var resultado = Result<int>.AddSuccessResult(idMoq);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Should().Be(idMoq);
            resultado.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Dado_Result_CuandoAgregarMensajeDeError_EntoncesDeberiaAgregarError()
        {
            // Arrange
            var idMoq = 1;
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");

            // Act
            var resultado = Result<int>.AddFailureResult(idMoq, result1.Key, result1.Message);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Value.Should().Be(idMoq);
            resultado.Errors.Should().HaveCount(1);
            resultado.Errors.Should().ContainKey(result1.Key);
            resultado.Errors[result1.Key].Should().Be(result1.Message);
        }

        [Fact]
        public void Dado_Result_CuandoAgregarMensajesDeError_EntoncesDeberiaAgregarErrores()
        {
            // Arrange
            var idMoq = 1;
            (string Key, string Message) result1 = ResultErrorMessageFactory.GetMessage("Categoria.NoEncontrada");
            (string Key, string Message) result2 = ResultErrorMessageFactory.GetMessage("Producto.NoEncontrado");
            (string Key, string Message) result3 = ResultErrorMessageFactory.GetMessage("Pedido.NoEncontrado");

            var errors = new Dictionary<string, object>
            {
                { result1.Key, result1.Message },
                { result2.Key, result2.Message },
                { result3.Key, result3.Message },
            };

            // Act
            var resultado = Result<int>.AddFailureResult(idMoq, errors);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Value.Should().Be(idMoq);
            resultado.Errors.Should().HaveCount(3);
            resultado.Errors.Should().ContainKeys(result1.Key, result2.Key, result3.Key);
            resultado.Errors[result1.Key].Should().Be(result1.Message);
            resultado.Errors[result2.Key].Should().Be(result2.Message);
            resultado.Errors[result3.Key].Should().Be(result3.Message);
        }
    }
}
