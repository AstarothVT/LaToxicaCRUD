using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LaToxicaCRUD.Models;
using LaToxicaCRUD.Services;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPlatoService _platoService;

        public PedidosController(IPedidoService pedidoService, IPlatoService platoService)
        {
            _pedidoService = pedidoService;
            _platoService = platoService;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.ListarAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Platos = await _platoService.ListarAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string cliente, List<int> platoId, List<int> cantidad)
        {
            var pedido = new Pedido
            {
                Cliente = cliente,
                Detalles = new List<DetallePedido>()
            };

            if (platoId != null)
            {
                for (int i = 0; i < platoId.Count; i++)
                {
                    if (cantidad[i] <= 0) continue;

                    var plato = await _platoService.ObtenerAsync(platoId[i]);
                    if (plato == null) continue;

                    pedido.Detalles.Add(new DetallePedido
                    {
                        PlatoId = plato.IdPlato,
                        Cantidad = cantidad[i],
                        Subtotal = plato.Precio * cantidad[i]
                    });
                }
            }

            if (pedido.Detalles.Count == 0)
            {
                ModelState.AddModelError("", "Debe agregar al menos un plato al pedido.");
                ViewBag.Platos = await _platoService.ListarAsync();
                return View();
            }

            var ok = await _pedidoService.CrearAsync(pedido);
            if (ok)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Ocurrio un error al registrar el pedido.");
            ViewBag.Platos = await _platoService.ListarAsync();
            return View();
        }
    }
}
