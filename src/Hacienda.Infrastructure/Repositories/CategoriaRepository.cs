using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;

namespace Hacienda.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly List<CategoriaProducto> _categorias;
    private int _nextId = 1; // Variable para generar nuevos IDs simulados

    public CategoriaRepository()
    {
        _categorias = new List<CategoriaProducto>();
    }

    public async Task<IList<CategoriaProducto>> GetAllAsync()
    {
        // Simulamos una operación asíncrona de creación, como una inserción en la base de datos
        await Task.Delay(100);

        if (_categorias.Any())
        {
            return _categorias;
        }
        else
        {
            // Crea una lista ficticia de categorías
            _categorias.Add(new CategoriaProducto() { Id = _nextId++, Nombre = "Electrónica", Descripcion = "Electrónica" });
            _categorias.Add(new CategoriaProducto() { Id = _nextId++, Nombre = "Ropa", Descripcion = "Ropa" });
            _categorias.Add(new CategoriaProducto() { Id = _nextId++, Nombre = "Hogar", Descripcion = "Hogar" });
        }

        // Retornamos la lista de categorías almacenada en el repositorio
        return _categorias;
    }

    public async Task<CategoriaProducto> GetAsync(int id)
    {
        // Simulamos una operación asíncrona de búsqueda, como una consulta en la base de datos
        await Task.Delay(100);

        // Buscamos la categoría por ID en la lista
        var categoria = _categorias.FirstOrDefault(c => c.Id == id);

        // Si la categoría se encontró, la devolvemos; de lo contrario, devolvemos null
        //TODO: poner un NotFoundException
        return categoria;
    }

    public async Task<Result<int>> InsAsync(CategoriaProducto categoria)
    {
        // Simulamos una operación asíncrona de creación, como una inserción en la base de datos
        await Task.Delay(100);

        // Agregamos la nueva categoría al repositorio
        _categorias.Add(categoria);

        //Simulamos que ha ido correctamente
        var result = new Result<int>(categoria.Id);

        return result;
    }
}