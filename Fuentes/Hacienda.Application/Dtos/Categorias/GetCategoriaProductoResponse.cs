using Hacienda.Application.Dtos.Primitives;

namespace Hacienda.Application.Dtos
{
    public class GetCategoriaProductoResponse
    {
        public CategoriaProductoIdResponse Id { get; set; }

        public string Nombre { get; set; }
    }
}
