namespace Hacienda.Domain.ValueObjects;

/// <summary>
/// Se podría utilizar por ejemplo, en el caso detalle de un pedido o productos, para el precio.  
/// Se trata de un Value object, ya que de por sí no nos dice nada, a quién aplica, etcétera.
/// Los records son simples, pero no permite validaciones, se deberían hacer en el padre.
/// </summary>
public record class Dinero(string moneda, decimal cantidad);