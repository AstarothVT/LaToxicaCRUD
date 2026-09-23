using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LaToxicaCRUD.Models;
using LaToxicaCRUD.Services;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class PlatosController : Controller
    {
        private readonly IPlatoService _platoService;
        private readonly ICategoriaService _categoriaService;

        public PlatosController(IPlatoService platoService, ICategoriaService categoriaService)
        {
            _platoService = platoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var platos = await _platoService.ListarAsync();
            return View(platos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await _categoriaService.ListarAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plato plato)
        {
            if (ModelState.IsValid)
            {
                var ok = await _platoService.CrearAsync(plato);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ocurrio un error al guardar el plato. Intente nuevamente.");
            }

            ViewBag.Categorias = await _categoriaService.ListarAsync();
            return View(plato);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var plato = await _platoService.ObtenerAsync(id.Value);
            if (plato == null)
                return NotFound();

            ViewBag.Categorias = await _categoriaService.ListarAsync();
            return View(plato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Plato plato)
        {
            if (id != plato.IdPlato)
                return NotFound();

            if (ModelState.IsValid)
            {
                var ok = await _platoService.ActualizarAsync(plato);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ocurrio un error al actualizar el plato. Intente nuevamente.");
            }

            ViewBag.Categorias = await _categoriaService.ListarAsync();
            return View(plato);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var plato = await _platoService.ObtenerAsync(id.Value);
            if (plato == null)
                return NotFound();

            return View(plato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _platoService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
