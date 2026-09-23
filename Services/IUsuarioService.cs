using System.Threading.Tasks;

namespace LaToxicaCRUD.Services
{
    public interface IUsuarioService
    {
        Task<bool> LoginAsync(string usuario, string password);
    }
}
