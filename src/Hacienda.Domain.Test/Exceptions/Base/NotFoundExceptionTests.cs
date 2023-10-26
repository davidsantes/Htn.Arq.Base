﻿using FluentAssertions;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Exceptions.Base;
using Xunit;

namespace Hacienda.Domain.Test.Exceptions.Base;

[Trait("Exceptions", "Base")]
public class NotFoundExceptionTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        // Arrange
        var id = 456;

        // Act
        var exception = new NotFoundException(typeof(CategoriaProducto), id);

        // Assert
        exception.Message.Should().Contain("Entidad no encontrada");
    }

    [Fact]
    public void ParameterizedConstructor_SetsCustomMessage()
    {
        // Arrange
        var id = 456;

        // Act
        var exception = new NotFoundException(typeof(CategoriaProducto), id);

        // Assert
        exception.Message.Should().Contain("Entidad no encontrada");
    }
}