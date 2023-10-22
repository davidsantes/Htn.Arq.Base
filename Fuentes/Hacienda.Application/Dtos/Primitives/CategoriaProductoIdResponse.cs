namespace Hacienda.Application.Dtos.Primitives
{
    public class CategoriaProductoIdResponse
    {
        public string Valor { get; set; }

        public CategoriaProductoIdResponse(string valor)
        {
            Valor = valor;
        }
    }
}