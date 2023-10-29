using Hacienda.Domain.Primitives;
using Hacienda.Domain.Results;

namespace Hacienda.Domain.ValueObjects;

public sealed class Nombre : ValueObject
{
    public const int MaxLenth = 50;

    private Nombre(string value)
    {
        Value = value;
    }

    public string Value { get;  }

    public static ResultToReturnWithoutObject Crear(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return ResultToReturnWithoutObject.AddFailureResult("Cliente.NombreVacio", "Nombre no puede ser vacío");
        }
        return ResultToReturnWithoutObject.AddSuccessResult();
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
