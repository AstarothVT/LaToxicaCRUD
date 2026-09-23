using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ListarAsync();
        Task<Categoria> ObtenerAsync(int id);
        Task<bool> CrearAsync(Categoria categoria);
        Task<bool> ActualizarAsync(Categoria categoria);
        Task<bool> EliminarAsync(int id);
    }
}
