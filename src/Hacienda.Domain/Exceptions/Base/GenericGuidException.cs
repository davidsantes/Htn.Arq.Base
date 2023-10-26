namespace Hacienda.Domain.Exceptions.Base;

/// <summary>
/// Excepción genérica con un guid de seguimiento.
/// Envuelve una excepción estándar.
/// </summary>
[Serializable]
public class GenericGuidException : Exception
{
    public Guid Guid { get; set; }

    public GenericGuidException(Exception exception) : base("Ha ocurrido un error.", exception)
    {
        Guid = Guid.NewGuid();
    }

    public override string Message => $"Ha ocurrido un error con el siguiente código de error: {Guid}. Para más detalles consulte con su administrador.";

    public override string ToString()
    {
        return "Exception GUID: " + Guid + Environment.NewLine + base.ToString();
    }
}