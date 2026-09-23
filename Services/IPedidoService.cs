using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ListarAsync();
        Task<bool> CrearAsync(Pedido pedido);
    }
}
