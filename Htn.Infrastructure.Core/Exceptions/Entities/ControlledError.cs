namespace Htn.Infrastructure.Core.Exceptions.Entities
{
    /// <summary>
    /// Error controlado para devolución al cliente
    /// </summary>
    public class ControlledError
    {
        public string Codigo { get; set; }

        public string Descripcion { get; set; }
    }
}