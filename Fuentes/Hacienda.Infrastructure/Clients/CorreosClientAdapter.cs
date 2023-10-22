using Hacienda.Application.Clients;
using Hacienda.Domain.Entities;

namespace Hacienda.Infrastructure.Clients
{
    public class CorreosClientAdapter : ICorreosClientAdapter
    {
        /// <summary>
        /// TODO: Revisar uso de httpclient:
        /// https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet
        /// Replacing Named Clients With Typed Clients
        /// Why You Should Avoid Typed Clients In Singleton Services (indicarlo!!!!)
        /// </summary>
        public async Task<Result<bool>> InsAsync()
        {
            // Simulamos una operación asíncrona de creación, como una inserción en la base de datos
            await Task.Delay(100);

            // Llamada al envío de correos

            //Simulamos que ha ido correctamente
            var envioRealizado = true;
            var result = new Result<bool>(envioRealizado);

            return result;
        }
    }
}