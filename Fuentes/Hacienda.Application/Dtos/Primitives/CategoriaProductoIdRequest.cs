namespace Hacienda.Application.Dtos.Primitives
{
    public class CategoriaProductoIdRequest
    {
        public string Valor { get; set; }

        public CategoriaProductoIdRequest(string valor)
        {
            Valor = valor;
        }
    }
}