using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ToxicaContext _context;

        public UsuarioRepository(ToxicaContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidarCredencialesAsync(string usuario, string password)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == usuario && u.Password == password);
        }
    }
}
