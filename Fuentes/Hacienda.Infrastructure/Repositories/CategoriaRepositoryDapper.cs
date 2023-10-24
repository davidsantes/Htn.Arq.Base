using Dapper;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Hacienda.Infrastructure.DbContextDapper;
using System.Data;

namespace Hacienda.Infrastructure.Repositories
{
    public class CategoriaRepositoryDapper : ICategoriaRepository
    {
        private const string GetCategoriaStoredProcedure = "GetCategoria";
        private readonly IConnectionFactory _connectionFactory;

        public CategoriaRepositoryDapper(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IList<CategoriaProducto>> GetAllAsync()
        {
            var sql = @"
                SELECT IdCategoriaProducto AS Id,
                       Nombre,
                       Descripcion
                FROM CategoriasProductos";

            using (var connection = _connectionFactory.GetOpenConnection())
            {
                var categorias = await connection.QueryAsync<CategoriaProducto>(sql);
                return categorias.ToList();
            }
        }

        public async Task<CategoriaProducto> GetAsync(int id)
        {
            var sql = @"
                SELECT IdCategoriaProducto AS Id,
                       Nombre,
                       Descripcion
                FROM CategoriasProductos
                WHERE IdCategoriaProducto = @Id";

            using (var connection = _connectionFactory.GetOpenConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<CategoriaProducto>(
                    GetCategoriaStoredProcedure,
                    new { Id = id }, // Parámetros del procedimiento almacenado
                    commandType: CommandType.StoredProcedure // Especifica que es un procedimiento almacenado
                );

                //Si se quiere llamar directamente:
                //var categoria = await connection.QuerySingleOrDefaultAsync<CategoriaProducto>(sql, new { Id = id });
                return categoria;
            }
        }

        public async Task<Result<int>> InsAsync(CategoriaProducto categoria)
        {
            var sql = @"
                INSERT INTO CategoriasProductos (Nombre, Descripcion, FechaAlta)
                VALUES (@Nombre, @Descripcion, @FechaAlta);
                SELECT SCOPE_IDENTITY();";

            using (var connection = _connectionFactory.GetOpenConnection())
            {
                categoria.Id = await connection.ExecuteScalarAsync<int>(sql, categoria);
            }

            return new Result<int>(categoria.Id);
        }
    }
}