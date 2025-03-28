using ConcertoAPI.Data;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertoAPI.Services
{
    public class BigliettoService : IBigliettoService
    {
        private readonly ApplicationDbContext _context;

        public BigliettoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Biglietto>> GetBigliettiByUserAsync(string userId)
        {
            return await _context.Biglietti
                .Where(b => b.UserId == userId)
                .Include(b => b.Evento)
                .ToListAsync();
        }

        public async Task<Biglietto> AcquistaBigliettoAsync(Guid eventoId, string userId)
        {
            var biglietto = new Biglietto
            {
                EventoId = eventoId,
                UserId = userId,
                DataAcquisto = DateTime.UtcNow
            };

            _context.Biglietti.Add(biglietto);
            await _context.SaveChangesAsync();
            return biglietto;
        }

        public async Task<IEnumerable<Biglietto>> GetAllBigliettiAsync()
        {
            return await _context.Biglietti.Include(b => b.Evento).ToListAsync();
        }
    }

}
