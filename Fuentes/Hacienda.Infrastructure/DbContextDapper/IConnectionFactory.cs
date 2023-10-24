using Microsoft.Data.SqlClient;

namespace Hacienda.Infrastructure.DbContextDapper
{
    public interface IConnectionFactory
    {
        SqlConnection GetOpenConnection();
    }
}