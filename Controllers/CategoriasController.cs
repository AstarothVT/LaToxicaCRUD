using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ToxicaContext _context;

        public CategoriasController(ToxicaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Platos)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
            return View(categorias);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _context.Categorias
                .Include(c => c.Platos)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            await ValidarNombreRepetido(categoria);

            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Categoría registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);

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

            await ValidarNombreRepetido(categoria);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Categorias.AnyAsync(c => c.IdCategoria == id))
                        return NotFound();
                    throw;
                }

                TempData["Exito"] = "Categoría actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _context.Categorias
                .Include(c => c.Platos)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Platos)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
                return RedirectToAction(nameof(Index));

            // No se puede borrar una categoría que todavía tiene platos
            if (categoria.Platos.Any())
            {
                TempData["Error"] = "No se puede eliminar la categoría porque todavía tiene platos registrados.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["Exito"] = "Categoría eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // Evita dos categorías con el mismo nombre
        private async Task ValidarNombreRepetido(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                return;

            var existe = await _context.Categorias.AnyAsync(c =>
                c.Nombre == categoria.Nombre && c.IdCategoria != categoria.IdCategoria);

            if (existe)
                ModelState.AddModelError(nameof(Categoria.Nombre), "Ya existe una categoría con ese nombre");
        }
    }
}
