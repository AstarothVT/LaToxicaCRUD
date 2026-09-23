using System.Threading.Tasks;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ValidarCredencialesAsync(string usuario, string password);
    }
}
