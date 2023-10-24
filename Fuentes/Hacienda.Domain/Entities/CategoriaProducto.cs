namespace Hacienda.Domain.Entities
{
    public class CategoriaProducto
    {
        //TODO: revisar How To Use Domain-Driven Design In Clean Architecture
        //https://www.youtube.com/watch?v=1Lcr2c3MVF4
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}