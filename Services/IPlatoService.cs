using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    public interface IPlatoService
    {
        Task<List<Plato>> ListarAsync();
        Task<Plato> ObtenerAsync(int id);
        Task<bool> CrearAsync(Plato plato);
        Task<bool> ActualizarAsync(Plato plato);
        Task<bool> EliminarAsync(int id);
    }
}
