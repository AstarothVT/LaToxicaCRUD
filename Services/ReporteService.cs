using System.Text;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace LaToxicaCRUD.Services
{
    public class ReporteService
    {
        // Stock igual o menor a este número se considera "bajo"
        public const int StockBajo = 10;

        private readonly ToxicaContext _context;

        public ReporteService(ToxicaContext context)
        {
            _context = context;
        }

        public async Task<ReporteViewModel> GenerarAsync(ReporteViewModel filtros)
        {
            var consulta = _context.Platos
                .Include(p => p.Categoria)
                .AsQueryable();

            if (filtros.CategoriaId.HasValue)
                consulta = consulta.Where(p => p.CategoriaId == filtros.CategoriaId.Value);

            if (filtros.PrecioMin.HasValue)
                consulta = consulta.Where(p => p.Precio >= filtros.PrecioMin.Value);

            if (filtros.PrecioMax.HasValue)
                consulta = consulta.Where(p => p.Precio <= filtros.PrecioMax.Value);

            if (filtros.SoloStockBajo)
                consulta = consulta.Where(p => p.Stock <= StockBajo);

            filtros.Platos = await consulta
                .OrderBy(p => p.Categoria.Nombre)
                .ThenBy(p => p.Nombre)
                .ToListAsync();

            // El resumen por categoría se arma en memoria con los platos ya filtrados
            filtros.Resumen = filtros.Platos
                .GroupBy(p => p.Categoria.Nombre)
                .Select(g => new ResumenCategoria
                {
                    Categoria = g.Key,
                    CantidadPlatos = g.Count(),
                    Unidades = g.Sum(p => p.Stock),
                    Valor = g.Sum(p => p.Precio * p.Stock)
                })
                .OrderBy(r => r.Categoria)
                .ToList();

            return filtros;
        }

        // Exporta el reporte filtrado a CSV (se abre en Excel)
        public byte[] ExportarCsv(ReporteViewModel reporte)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id;Plato;Categoria;Precio;Stock;Valor en inventario");

            foreach (var p in reporte.Platos)
            {
                sb.AppendLine(string.Join(";",
                    p.IdPlato,
                    Limpiar(p.Nombre),
                    Limpiar(p.Categoria.Nombre),
                    p.Precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                    p.Stock,
                    (p.Precio * p.Stock).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
            }

            sb.AppendLine($";;;TOTAL;{reporte.TotalUnidades};{reporte.ValorInventario.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}");

            // BOM UTF-8 para que Excel muestre bien las tildes
            var contenido = Encoding.UTF8.GetBytes(sb.ToString());
            return Encoding.UTF8.GetPreamble().Concat(contenido).ToArray();
        }

        private static string Limpiar(string texto)
        {
            return (texto ?? "").Replace(";", ",").Replace("\n", " ").Replace("\r", " ");
        }
    }
}
