namespace LaToxicaCRUD.Models
{
    // Filtros + resultados del reporte de inventario
    public class ReporteViewModel
    {
        public int? CategoriaId { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public bool SoloStockBajo { get; set; }

        public List<Plato> Platos { get; set; } = new List<Plato>();
        public List<ResumenCategoria> Resumen { get; set; } = new List<ResumenCategoria>();

        public int TotalPlatos => Platos.Count;
        public int TotalUnidades => Platos.Sum(p => p.Stock);
        public decimal ValorInventario => Platos.Sum(p => p.Precio * p.Stock);
    }

    public class ResumenCategoria
    {
        public string Categoria { get; set; }
        public int CantidadPlatos { get; set; }
        public int Unidades { get; set; }
        public decimal Valor { get; set; }
    }
}
