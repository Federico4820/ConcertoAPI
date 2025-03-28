using ConcertoAPI.Data;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertoAPI.Services
{
    public class BigliettoService : IBigliettoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BigliettoService> _logger;

        public BigliettoService(ApplicationDbContext context, ILogger<BigliettoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Biglietto>> GetBigliettiByUserAsync(string userId)
        {
            try
            {
                return await _context.Biglietti
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Evento)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero dei biglietti per l'utente con ID {userId}.");
                throw;
            }
        }

        public async Task<Biglietto> AcquistaBigliettoAsync(Guid eventoId, string userId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'acquisto di un biglietto per l'evento con ID {eventoId} da parte dell'utente {userId}.");
                throw;
            }
        }

        public async Task<IEnumerable<Biglietto>> GetAllBigliettiAsync()
        {
            try
            {
                return await _context.Biglietti.Include(b => b.Evento).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero di tutti i biglietti.");
                throw;
            }
        }
    }


}
