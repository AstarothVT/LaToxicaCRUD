using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data.Repositories
{
    public interface IPlatoRepository
    {
        Task<List<Plato>> ObtenerTodosAsync();
        Task<Plato> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Plato plato);
        Task ActualizarAsync(Plato plato);
        Task EliminarAsync(int id);
    }
}
