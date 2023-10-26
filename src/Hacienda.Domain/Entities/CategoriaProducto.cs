namespace Hacienda.Domain.Entities;

public class CategoriaProducto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaAlta { get; set; } = DateTime.Now;

    //TODO: revisar How To Use Domain-Driven Design In Clean Architecture, ¿crear los setters privados?
    //https://www.youtube.com/watch?v=1Lcr2c3MVF4
    //public int Id { get; private set; }
    //public string Nombre { get; private set; }
    //public string Descripcion { get; private set; }
    //public DateTime FechaAlta { get; private set; }

    //private CategoriaProducto(int id, string nombre, string descripcion, DateTime fechaAlta)
    //{
    //    Id = id;
    //    Nombre = nombre;
    //    Descripcion = descripcion;
    //    FechaAlta = fechaAlta;
    //}

    //public static CategoriaProducto CrearNuevaCategoria(int id, string nombre, string descripcion)
    //{
    //    return new CategoriaProducto(id, nombre, descripcion, DateTime.Now);
    //}
}