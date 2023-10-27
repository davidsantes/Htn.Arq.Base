namespace Hacienda.Application.Dtos;

public class InsertCategoriaProductoRequest
{
    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public InsertCategoriaProductoRequest(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
}