using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Data.Repositories;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    // Capa de negocio: aqui van las reglas del sistema. El controlador
    // nunca habla directo con Entity Framework, siempre pasa por aqui.
    public class PlatoService : IPlatoService
    {
        private readonly IPlatoRepository _repositorio;
        private readonly ILogService _log;

        public PlatoService(IPlatoRepository repositorio, ILogService log)
        {
            _repositorio = repositorio;
            _log = log;
        }

        public async Task<List<Plato>> ListarAsync()
        {
            try
            {
                return await _repositorio.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PlatoService.ListarAsync", ex);
                return new List<Plato>();
            }
        }

        public async Task<Plato> ObtenerAsync(int id)
        {
            try
            {
                return await _repositorio.ObtenerPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PlatoService.ObtenerAsync", ex);
                return null;
            }
        }

        public async Task<bool> CrearAsync(Plato plato)
        {
            try
            {
                await _repositorio.AgregarAsync(plato);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PlatoService.CrearAsync", ex);
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Plato plato)
        {
            try
            {
                await _repositorio.ActualizarAsync(plato);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PlatoService.ActualizarAsync", ex);
                return false;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                await _repositorio.EliminarAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PlatoService.EliminarAsync", ex);
                return false;
            }
        }
    }
}
