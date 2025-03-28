using ConcertoAPI.Data;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertoAPI.Services
{
    public class EventoService : IEventoService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EventoService> _logger;

    public EventoService(ApplicationDbContext context, ILogger<EventoService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Evento>> GetEventiAsync()
    {
        try
        {
            return await _context.Eventi.Include(e => e.Artista).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero degli eventi.");
            throw;
        }
    }

    public async Task<Evento> GetEventoByIdAsync(Guid id)
    {
        try
        {
            return await _context.Eventi.Include(e => e.Artista).FirstOrDefaultAsync(e => e.EventoId == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore nel recupero dell'evento con ID {id}.");
            throw;
        }
    }

    public async Task<Evento> CreateEventoAsync(Evento evento)
    {
        try
        {
            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante la creazione di un nuovo evento.");
            throw;
        }
    }

    public async Task<Evento> UpdateEventoAsync(Guid id, Evento evento)
    {
        try
        {
            var eventoEsistente = await _context.Eventi.FindAsync(id);
            if (eventoEsistente == null) return null;

            eventoEsistente.Titolo = evento.Titolo;
            eventoEsistente.Data = evento.Data;
            eventoEsistente.Luogo = evento.Luogo;
            eventoEsistente.ArtistaId = evento.ArtistaId;

            await _context.SaveChangesAsync();
            return eventoEsistente;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante l'aggiornamento dell'evento con ID {id}.");
            throw;
        }
    }

    public async Task<bool> DeleteEventoAsync(Guid id)
    {
        try
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null) return false;

            _context.Eventi.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante l'eliminazione dell'evento con ID {id}.");
            throw;
        }
    }
}


}
