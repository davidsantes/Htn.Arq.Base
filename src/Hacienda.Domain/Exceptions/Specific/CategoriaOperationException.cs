using Hacienda.Domain.Exceptions.Base;

namespace Hacienda.Domain.Exceptions.Specific;

public class CategoriaOperationException : OperationException
{
    public int? CategoriaId { get; }

    public CategoriaOperationException(int? categoriaId)
        : base($"Categoría con ID {categoriaId} ha realizado una operación no consistente.")
    {
        CategoriaId = categoriaId;
    }

    public CategoriaOperationException()
    : base("Se ha realizado una operación no válida con la categoría.")
    {

    }
}