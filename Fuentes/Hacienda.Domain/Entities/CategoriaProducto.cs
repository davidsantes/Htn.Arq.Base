using Hacienda.Domain.Primitives;

namespace Hacienda.Domain.Entities
{
    public class CategoriaProducto
    {
        public CategoriaProductoId Id { get; private set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateOnly FechaAlta { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public CategoriaProducto(int id)
        {
            Id = new CategoriaProductoId(id);
        }
    }
}