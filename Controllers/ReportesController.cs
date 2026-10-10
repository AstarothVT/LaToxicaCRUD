using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Models;
using LaToxicaCRUD.Services;

namespace LaToxicaCRUD.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly ToxicaContext _context;
        private readonly ReporteService _reporteService;

        public ReportesController(ToxicaContext context, ReporteService reporteService)
        {
            _context = context;
            _reporteService = reporteService;
        }

        // Reporte de inventario con filtros
        public async Task<IActionResult> Platos(ReporteViewModel filtros)
        {
            if (filtros.PrecioMin.HasValue && filtros.PrecioMax.HasValue &&
                filtros.PrecioMin > filtros.PrecioMax)
            {
                ModelState.AddModelError("PrecioMin", "El precio mínimo no puede ser mayor al máximo");
                filtros.PrecioMin = null;
                filtros.PrecioMax = null;
            }

            var reporte = await _reporteService.GenerarAsync(filtros);
            await CargarCategorias(filtros.CategoriaId);
            return View(reporte);
        }

        // Descarga el mismo reporte (con los mismos filtros) en CSV
        public async Task<IActionResult> ExportarCsv(ReporteViewModel filtros)
        {
            var reporte = await _reporteService.GenerarAsync(filtros);
            var archivo = _reporteService.ExportarCsv(reporte);
            var nombre = $"Reporte_Platos_{DateTime.Now:yyyyMMdd_HHmm}.csv";
            return File(archivo, "text/csv", nombre);
        }

        private async Task CargarCategorias(int? seleccionada)
        {
            ViewBag.Categorias = new SelectList(
                await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync(),
                "IdCategoria", "Nombre", seleccionada);
        }
    }
}
