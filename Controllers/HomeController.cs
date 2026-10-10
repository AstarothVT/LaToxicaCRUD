using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToxicaContext _context;

        public HomeController(ToxicaContext context)
        {
            _context = context;
        }

        // Página de inicio (pública)
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalPlatos = await _context.Platos.CountAsync();
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();

            // Los 6 platos más caros se muestran como "los más tóxicos"
            var destacados = await _context.Platos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.Precio)
                .Take(6)
                .ToListAsync();

            return View(destacados);
        }

        // Carta completa agrupada por categoría (pública)
        public async Task<IActionResult> Carta()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Platos)
                .Where(c => c.Platos.Any())
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return View(categorias);
        }

        // Platos típicos de una hamburguesería (pública)
        public IActionResult Tipicos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
