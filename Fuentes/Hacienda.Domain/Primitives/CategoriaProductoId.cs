namespace Hacienda.Domain.Primitives
{
    /// <summary>
    /// Este ejemplo es sencillo, pero puede venir bien para identificadores complejos.
    /// </summary>
    public class CategoriaProductoId
    {
        public int Valor { get; private set; }

        public CategoriaProductoId(int valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("El ID de la categoría debe ser un valor positivo.");
            }
            Valor = valor;
        }
    }
}