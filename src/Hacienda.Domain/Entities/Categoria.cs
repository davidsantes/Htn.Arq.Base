using Hacienda.Domain.Primitives;

namespace Hacienda.Domain.Entities;

public sealed class Categoria : Entity
{
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public DateTime FechaAlta { get; private set; }

    /// <summary>
    /// Necesario solo si se usa Dapper
    /// </summary>
    private Categoria() : base(Guid.NewGuid())
    {

    }

    private Categoria(Guid id
        , string nombre
        , string descripcion
        , DateTime fechaAlta)
        : base(id)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        FechaAlta = fechaAlta;
    }

    public static Categoria Crear(string nombre, string descripcion)
    {
        return new Categoria(Guid.NewGuid(), nombre, descripcion, DateTime.Now);
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