﻿using Dapper;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Hacienda.Domain.ResultErrors;
using Hacienda.Infrastructure.DbContextDapper;
using System.Data;

namespace Hacienda.Infrastructure.Repositories;

/// <summary>
/// Prueba de concepto de uso de dapper
/// </summary>
public class CategoriaRepositoryPruebaDapper : ICategoriaRepositoryPruebaDapper
{
    private const string GetCategoriaStoredProcedure = "Get_Categoria_By_Id";
    private readonly IConnectionFactory _connectionFactory;

    public CategoriaRepositoryPruebaDapper(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<CategoriaProducto> GetAsync(int id)
    {
        //Cambiar si quiere hacerse una prueba de concepto query directa vs procedimiento almacenado:
        var ejecutarConProcedimientoAlmacenado = false;
        if (ejecutarConProcedimientoAlmacenado)
        {
            using (var connection = _connectionFactory.GetOpenConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<CategoriaProducto>(
                    GetCategoriaStoredProcedure,
                    new { Id = id }, // Parámetros del procedimiento almacenado
                    commandType: CommandType.StoredProcedure // Especifica que es un procedimiento almacenado
                );
                return categoria;
            }
        }
        else
        {
            var sql = @"
                SELECT IdCategoriaProducto AS Id,
                       Nombre,
                       Descripcion
                FROM CategoriasProductos
                WHERE IdCategoriaProducto = @Id";

            using (var connection = _connectionFactory.GetOpenConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<CategoriaProducto>(sql, new { Id = id });
                return categoria;
            }
        }
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public Task<Result<int>> UpdAsync(CategoriaProducto categoria)
    {
        //Como es una prueba de concepto no se ha completado
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Result<int>> DelAsync(int id)
    {
        //Como es una prueba de concepto no se ha completado
        throw new NotImplementedException();
    }
}