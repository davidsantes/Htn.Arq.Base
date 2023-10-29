using FluentAssertions;
using Hacienda.Domain.Primitives;
using Xunit;

namespace Hacienda.Domain.Test.Primitives;

[Trait("Primitives", "ValueObjects")]
public class ValueObjectTests
{
    [Fact]
    public void ValueObjectsWithSameAtomicValuesShouldBeEqual()
    {
        // Arrange
        var valueObject1 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");
        var valueObject2 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");

        // Act & Assert
        valueObject1.Should().Be(valueObject2);
    }

    [Fact]
    public void ValueObjectsWithDifferentAtomicValuesShouldNotBeEqual()
    {
        // Arrange
        var valueObject1 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");
        var valueObject2 = new SampleValueObject(new Guid("22222222-2222-2222-2222-222222222222"), "Value2");

        // Act & Assert
        valueObject1.Should().NotBe(valueObject2);
    }

    [Fact]
    public void ValueObjectsWithSameAtomicValuesShouldHaveSameHashCode()
    {
        // Arrange
        var valueObject1 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");
        var valueObject2 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");

        // Act & Assert
        valueObject1.GetHashCode().Should().Be(valueObject2.GetHashCode());
    }

    [Fact]
    public void ValueObjectsWithDifferentAtomicValuesShouldHaveDifferentHashCode()
    {
        // Arrange
        var valueObject1 = new SampleValueObject(new Guid("11111111-1111-1111-1111-111111111111"), "Value1");
        var valueObject2 = new SampleValueObject(new Guid("22222222-2222-2222-2222-222222222222"), "Value2");

        // Act & Assert
        valueObject1.GetHashCode().Should().NotBe(valueObject2.GetHashCode());
    }

    public class SampleValueObject : ValueObject
    {
        public SampleValueObject(Guid id, string value)
        {
            Id = id;
            Value = value;
        }

        public Guid Id { get; }
        public string Value { get; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Value;
        }
    }
}