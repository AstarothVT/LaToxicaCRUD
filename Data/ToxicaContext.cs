using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data
{
    public class ToxicaContext : DbContext
    {
        public ToxicaContext(DbContextOptions<ToxicaContext> options)
            : base(options)
        {
        }

        public DbSet<Plato> Platos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Un plato pertenece a una categoria
            modelBuilder.Entity<Plato>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Platos)
                .HasForeignKey(p => p.CategoriaId);

            // Un detalle de pedido pertenece a un pedido y a un plato
            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.PedidoId);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Plato)
                .WithMany()
                .HasForeignKey(d => d.PlatoId);
        }
    }
}
