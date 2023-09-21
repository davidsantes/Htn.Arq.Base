using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Dal.Repositories.Interfaces;

namespace Htn.Arq.Base.Dal.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<CategoriaProducto> _categorias;

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
                _categorias.Add(new CategoriaProducto { Id = 1, Nombre = "Electrónica" });
                _categorias.Add(new CategoriaProducto { Id = 2, Nombre = "Ropa" });
                _categorias.Add(new CategoriaProducto { Id = 3, Nombre = "Hogar" });
            }

            // Retornamos la lista de categorías almacenada en el repositorio
            return _categorias;
        }

        public async Task<Result<int>> InsAsync(CategoriaProducto categoria)
        {
            // Simulamos una operación asíncrona de creación, como una inserción en la base de datos
            await Task.Delay(100);

            // Generamos un nuevo ID (esto puede variar según tu base de datos)
            int newId = _categorias.Count + 1;
            categoria.Id = newId;

            // Agregamos la nueva categoría al repositorio
            _categorias.Add(categoria);

            //Simulamos que ha ido correctamente
            var result = new Result<int>(newId);

            return result;
        }
    }
}