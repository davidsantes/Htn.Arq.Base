namespace Hacienda.Domain.Entities;

public sealed class CategoriaProducto
{
    //TODO: revisar How To Use Domain-Driven Design In Clean Architecture, ¿crear los setters privados?
    //https://www.youtube.com/watch?v=1Lcr2c3MVF4
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public DateTime FechaAlta { get; private set; }

    private CategoriaProducto(string nombre, string descripcion, DateTime fechaAlta)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        FechaAlta = fechaAlta;
    }

    public static CategoriaProducto Crear(string nombre, string descripcion)
    {
        return new CategoriaProducto(nombre, descripcion, DateTime.Now);
    }

    public void CambiarNombre(string nuevoNombre)
    {
        //Aquí podría haber validaciones
        Nombre = nuevoNombre;
    }

    public void CambiarDescripcion(string nuevaDescripcion)
    {
        //Aquí podría haber validaciones
        Descripcion = nuevaDescripcion;
    }
}