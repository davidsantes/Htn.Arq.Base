namespace Hacienda.Domain.Primitives;

#nullable enable
/// <summary>
/// Sacado de: Milan Jovanović, cómo usar objetos de valor para resolver la obsesión primitiva | Arquitectura limpia, DDD:
/// <see href="https://www.youtube.com/watch?v=IlXnIe6p_Uk"/>
/// En caso de que no haga falta hacer validaciones internas, o se puedan jacer en el padre, mejor utilizar records.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int),
            HashCode.Combine);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}