namespace Hacienda.Domain.Exceptions.Base;

public class NotFoundException : Exception
{
    public Type EntityType { get; }
    public int EntityId { get; }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(Type entityType, int entityId) : base($"Entidad no encontrada: tipo {entityType.Name} con ID {entityId}")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}