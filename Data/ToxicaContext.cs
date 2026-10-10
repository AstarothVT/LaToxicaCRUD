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

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Plato> Platos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1 a N: una categoría tiene muchos platos.
            // Restrict = no deja borrar una categoría que todavía tiene platos.
            modelBuilder.Entity<Plato>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Platos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
