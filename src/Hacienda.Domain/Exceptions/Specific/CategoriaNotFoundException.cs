using Hacienda.Domain.Exceptions.Generic;

namespace Hacienda.Domain.Exceptions.Specific;

public class CategoriaNotFoundException : NotFoundException
{
    public int CategoriaId { get; }

    public CategoriaNotFoundException(int categoriaId)
        : base($"Categoría con ID {categoriaId} no encontrada")
    {
        CategoriaId = categoriaId;
    }
}