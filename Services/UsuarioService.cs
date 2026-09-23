using System;
using System.Threading.Tasks;
using LaToxicaCRUD.Data.Repositories;

namespace LaToxicaCRUD.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorio;
        private readonly ILogService _log;

        public UsuarioService(IUsuarioRepository repositorio, ILogService log)
        {
            _repositorio = repositorio;
            _log = log;
        }

        public async Task<bool> LoginAsync(string usuario, string password)
        {
            try
            {
                var encontrado = await _repositorio.ValidarCredencialesAsync(usuario, password);
                return encontrado != null;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("UsuarioService.LoginAsync", ex);
                return false;
            }
        }
    }
}
