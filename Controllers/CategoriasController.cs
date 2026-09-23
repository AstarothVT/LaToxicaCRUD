using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LaToxicaCRUD.Models;
using LaToxicaCRUD.Services;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ListarAsync();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var ok = await _categoriaService.CrearAsync(categoria);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ocurrio un error al guardar la categoria.");
            }
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _categoriaService.ObtenerAsync(id.Value);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
                return NotFound();

            if (ModelState.IsValid)
            {
                var ok = await _categoriaService.ActualizarAsync(categoria);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ocurrio un error al actualizar la categoria.");
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _categoriaService.ObtenerAsync(id.Value);
            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaService.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
