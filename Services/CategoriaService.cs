using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaToxicaCRUD.Data.Repositories;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repositorio;
        private readonly ILogService _log;

        public CategoriaService(ICategoriaRepository repositorio, ILogService log)
        {
            _repositorio = repositorio;
            _log = log;
        }

        public async Task<List<Categoria>> ListarAsync()
        {
            try
            {
                return await _repositorio.ObtenerTodasAsync();
            }
            catch (Exception ex)
            {
                _log.RegistrarError("CategoriaService.ListarAsync", ex);
                return new List<Categoria>();
            }
        }

        public async Task<Categoria> ObtenerAsync(int id)
        {
            try
            {
                return await _repositorio.ObtenerPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _log.RegistrarError("CategoriaService.ObtenerAsync", ex);
                return null;
            }
        }

        public async Task<bool> CrearAsync(Categoria categoria)
        {
            try
            {
                await _repositorio.AgregarAsync(categoria);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("CategoriaService.CrearAsync", ex);
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Categoria categoria)
        {
            try
            {
                await _repositorio.ActualizarAsync(categoria);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("CategoriaService.ActualizarAsync", ex);
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
                _log.RegistrarError("CategoriaService.EliminarAsync", ex);
                return false;
            }
        }
    }
}
