using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> ObtenerTodosAsync();
        Task<Pedido> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Pedido pedido);
    }
}
