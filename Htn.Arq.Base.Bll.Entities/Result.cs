using System.Collections.ObjectModel;

namespace Htn.Arq.Base.Bll.Entities
{
    /// <summary>
    /// Patrón result pattern, para poder devolver resultados controlados y tipados
    /// </summary>
    /// <typeparam name="T">Elemento a devolver</typeparam>
    public class Result<T>
    {
        public bool IsSuccess => !_errors.Any();
        public T Value { get; }
        public ReadOnlyCollection<string> Errors { get; }

        private List<string> _errors = new List<string>();

        public Result(T value)
        {
            Value = value;
            Errors = new ReadOnlyCollection<string>(_errors);
        }

        public void AddErrorMessage(string message)
        {
            _errors.Add(message);
        }
    }
}