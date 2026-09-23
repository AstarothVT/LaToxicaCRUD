using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaToxicaCRUD.Data.Repositories;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repositorio;
        private readonly ILogService _log;

        public PedidoService(IPedidoRepository repositorio, ILogService log)
        {
            _repositorio = repositorio;
            _log = log;
        }

        public async Task<List<Pedido>> ListarAsync()
        {
            try
            {
                return await _repositorio.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PedidoService.ListarAsync", ex);
                return new List<Pedido>();
            }
        }

        public async Task<bool> CrearAsync(Pedido pedido)
        {
            try
            {
                // Regla de negocio: el total se calcula sumando los subtotales de cada detalle
                if (pedido.Detalles != null)
                {
                    pedido.Total = pedido.Detalles.Sum(d => d.Subtotal);
                }

                await _repositorio.AgregarAsync(pedido);
                return true;
            }
            catch (Exception ex)
            {
                _log.RegistrarError("PedidoService.CrearAsync", ex);
                return false;
            }
        }
    }
}
