namespace Hacienda.Domain.Primitives;
#nullable enable
/// <summary>
/// Clase base abstracta para entidades que tienen una propiedad de identificador (Id).
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }

    /// <summary>
    /// Sobrecarga del operador de igualdad (==) para comparar dos entidades.
    /// </summary>
    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    /// <summary>
    /// Sobrecarga del operador de desigualdad (!=) para comparar dos entidades.
    /// </summary>
    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    /// <summary>
    /// Determina si la entidad es igual a otro objeto.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    /// <summary>
    /// Determina si la entidad es igual a otra entidad.
    /// </summary>
    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    /// <summary>
    /// Obtiene el código hash de la entidad.
    /// </summary>
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}