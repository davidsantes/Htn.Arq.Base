using FluentAssertions;
using Hacienda.Domain.Primitives;
using Xunit;

namespace Hacienda.Domain.Test.Primitives;

[Trait("Primitives", "Entities")]
public class EntityTests
{
    [Fact]
    public void EntitiesWithSameIdShouldBeEqual()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var entity1 = new EntityWithId(id);
        var entity2 = new EntityWithId(id);

        // Act & Assert
        entity1.Equals(entity2).Should().BeTrue();
    }

    [Fact]
    public void EntitiesWithDifferentIdsShouldNotBeEqual()
    {
        // Arrange
        var entity1 = new EntityWithId(Guid.NewGuid());
        var entity2 = new EntityWithId(Guid.NewGuid());

        // Act & Assert
        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void EntitiesWithSameIdShouldHaveSameHashCode()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var entity1 = new EntityWithId(id);
        var entity2 = new EntityWithId(id);

        // Act & Assert
        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }

    [Fact]
    public void EntitiesWithDifferentIdsShouldHaveDifferentHashCode()
    {
        // Arrange
        var entity1 = new EntityWithId(Guid.NewGuid());
        var entity2 = new EntityWithId(Guid.NewGuid());

        // Act & Assert
        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
    }

    public class EntityWithId : Entity
    {
        public EntityWithId(Guid id) : base(id)
        {
        }
    }
}