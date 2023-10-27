namespace Hacienda.Application.Dtos;

public class UpdateCategoriaProductoRequest
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }
}