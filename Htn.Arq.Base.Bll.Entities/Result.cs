namespace Htn.Arq.Base.Bll.Entities
{
    /// <summary>
    /// Patrón result pattern, para poder devolver resultados controlados y tipados
    /// </summary>
    /// <typeparam name="T">Elemento a devolver</typeparam>
    public class Result<T>
    {
        public bool IsSuccess => !Errors.Any();
        public T Value { get; }
        public IDictionary<string, object> Errors { get; }

        public Result(T value)
        {
            Value = value;
            Errors = new Dictionary<string, object>();
        }

        public void AddErrorMessage(string key, string message)
        {
            Errors[key] = message;
        }
    }
}