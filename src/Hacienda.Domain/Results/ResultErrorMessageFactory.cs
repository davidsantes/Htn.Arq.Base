namespace Hacienda.Domain.Results;

/// <summary>
/// Clase que genera mensajes con clave - valor. Pensado para utilizar con Result para errores controlados
/// Por ejemplo: que una entidad no exista y que se intente actualizar en base de datos, pero que no pase nada si no
/// hay filas afectadas.
/// </summary>
public class ResultErrorMessageFactory
{
    private static readonly Dictionary<string, string> ErrorMessages = new Dictionary<string, string>
    {
        { "Categoria.NoEncontrada", "La categoría no existe" },
        { "Producto.NoEncontrado", "El producto no existe" },
        { "Pedido.NoEncontrado", "El pedido no existe" },
    };

    public static (string Key, string Message) GetMessage(string entityKey)
    {
        if (ErrorMessages.TryGetValue(entityKey, out var message))
        {
            return (entityKey, message);
        }
        return (entityKey, "Error genérico");
    }
}