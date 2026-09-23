using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Data.Repositories
{
    // Esta clase es la unica que habla directo con Entity Framework (capa de datos)
    public class PlatoRepository : IPlatoRepository
    {
        private readonly ToxicaContext _context;

        public PlatoRepository(ToxicaContext context)
        {
            _context = context;
        }

        public async Task<List<Plato>> ObtenerTodosAsync()
        {
            return await _context.Platos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Plato> ObtenerPorIdAsync(int id)
        {
            return await _context.Platos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdPlato == id);
        }

        public async Task AgregarAsync(Plato plato)
        {
            _context.Platos.Add(plato);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Plato plato)
        {
            _context.Platos.Update(plato);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var plato = await _context.Platos.FindAsync(id);
            if (plato != null)
            {
                _context.Platos.Remove(plato);
                await _context.SaveChangesAsync();
            }
        }
    }
}
