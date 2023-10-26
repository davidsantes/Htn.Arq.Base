namespace Hacienda.Domain.ResultErrors;

/// <summary>
/// Clase que genera mensajes con clave - valor. Pensado para utilizar con Result para errores controlados
/// Por ejemplo: que una entidad no exista y que se intente actualizar en base de datos, pero que no pase nada si no
/// hay filas afectadas.
/// </summary>
public class ResultMessageFactory
{
    private static readonly Dictionary<string, string> ErrorMessages = new Dictionary<string, string>
{
    { "CategoriaNoEncontrada", "La categoría no existe" },
    { "ProductoNoEncontrado", "El producto no existe" },
    { "PedidoNoEncontrado", "El pedido no  no existe" },
};

    public static (string Key, string Message) CreateMessage(string entityKey)
    {
        if (ErrorMessages.TryGetValue(entityKey, out var message))
        {
            return (entityKey, message);
        }
        return (entityKey, "Error genérico");
    }
}