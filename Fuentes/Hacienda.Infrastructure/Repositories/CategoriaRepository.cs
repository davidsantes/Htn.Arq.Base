using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;

namespace Hacienda.Infrastructure.Repositories
{
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
                _categorias.Add(new CategoriaProducto(_nextId++) { Nombre = "Electrónica", Descripcion = "Electrónica" });
                _categorias.Add(new CategoriaProducto(_nextId++) { Nombre = "Ropa", Descripcion = "Ropa" });
                _categorias.Add(new CategoriaProducto(_nextId++) { Nombre = "Hogar", Descripcion = "Hogar" });
            }

            // Retornamos la lista de categorías almacenada en el repositorio
            return _categorias;
        }

        public async Task<Result<int>> InsAsync(CategoriaProducto categoria)
        {
            // Simulamos una operación asíncrona de creación, como una inserción en la base de datos
            await Task.Delay(100);

            // Agregamos la nueva categoría al repositorio
            _categorias.Add(categoria);

            //Simulamos que ha ido correctamente
            var result = new Result<int>(categoria.Id.Valor);

            return result;
        }
    }
}