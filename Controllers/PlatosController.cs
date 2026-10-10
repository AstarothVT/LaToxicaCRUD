using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class PlatosController : Controller
    {
        private readonly ToxicaContext _context;

        public PlatosController(ToxicaContext context)
        {
            _context = context;
        }

        // Listado con búsqueda por nombre y filtro por categoría
        public async Task<IActionResult> Index(string buscar, int? categoriaId)
        {
            var consulta = _context.Platos
                .Include(p => p.Categoria)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
                consulta = consulta.Where(p => p.Nombre.Contains(buscar));

            if (categoriaId.HasValue)
                consulta = consulta.Where(p => p.CategoriaId == categoriaId.Value);

            ViewBag.Buscar = buscar;
            ViewBag.Categorias = new SelectList(
                await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync(),
                "IdCategoria", "Nombre", categoriaId);

            var platos = await consulta
                .OrderBy(p => p.Categoria.Nombre)
                .ThenBy(p => p.Nombre)
                .ToListAsync();

            return View(platos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var plato = await _context.Platos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdPlato == id);

            if (plato == null)
                return NotFound();

            return View(plato);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View(new Plato());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plato plato)
        {
            await ValidarCategoria(plato);

            if (ModelState.IsValid)
            {
                _context.Add(plato);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Plato registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCategorias(plato.CategoriaId);
            return View(plato);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var plato = await _context.Platos.FindAsync(id);

            if (plato == null)
                return NotFound();

            await CargarCategorias(plato.CategoriaId);
            return View(plato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Plato plato)
        {
            if (id != plato.IdPlato)
                return NotFound();

            await ValidarCategoria(plato);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Platos.AnyAsync(p => p.IdPlato == id))
                        return NotFound();
                    throw;
                }

                TempData["Exito"] = "Plato actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            await CargarCategorias(plato.CategoriaId);
            return View(plato);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var plato = await _context.Platos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdPlato == id);

            if (plato == null)
                return NotFound();

            return View(plato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plato = await _context.Platos.FindAsync(id);

            if (plato != null)
            {
                _context.Platos.Remove(plato);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Plato eliminado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task CargarCategorias(int? seleccionada = null)
        {
            ViewBag.Categorias = new SelectList(
                await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync(),
                "IdCategoria", "Nombre", seleccionada);
        }

        // Revisa que la categoría elegida exista de verdad en la base de datos
        private async Task ValidarCategoria(Plato plato)
        {
            if (plato.CategoriaId > 0 &&
                !await _context.Categorias.AnyAsync(c => c.IdCategoria == plato.CategoriaId))
            {
                ModelState.AddModelError(nameof(Plato.CategoriaId), "La categoría seleccionada no existe");
            }
        }
    }
}
