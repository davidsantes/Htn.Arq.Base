using Hacienda.Application.Dtos.Primitives;

namespace Hacienda.Application.Dtos
{
    public class InsertCategoriaProductoRequest
    {
        public CategoriaProductoIdRequest Id { get; set; }

        public string Nombre { get; set; }
    }
}
