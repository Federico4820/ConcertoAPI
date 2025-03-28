using ConcertoAPI.Data;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertoAPI.Services
{
    public class EventoService : IEventoService
    {
        private readonly ApplicationDbContext _context;

        public EventoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> GetEventiAsync()
        {
            return await _context.Eventi.Include(e => e.Artista).ToListAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(Guid id)
        {
            return await _context.Eventi.Include(e => e.Artista).FirstOrDefaultAsync(e => e.EventoId == id);
        }

        public async Task<Evento> CreateEventoAsync(Evento evento)
        {
            _context.Eventi.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<Evento> UpdateEventoAsync(Guid id, Evento evento)
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

        public async Task<bool> DeleteEventoAsync(Guid id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null) return false;

            _context.Eventi.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
