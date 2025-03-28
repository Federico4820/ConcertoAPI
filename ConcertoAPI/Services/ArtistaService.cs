using ConcertoAPI.Data;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertoAPI.Services
{
    public class ArtistaService : IArtistaService
    {
        private readonly ApplicationDbContext _context;

        public ArtistaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artista>> GetArtistiAsync()
        {
            return await _context.Artisti.ToListAsync();
        }

        public async Task<Artista> GetArtistaByIdAsync(Guid id)
        {
            return await _context.Artisti.FindAsync(id);
        }

        public async Task<Artista> CreateArtistaAsync(Artista artista)
        {
            _context.Artisti.Add(artista);
            await _context.SaveChangesAsync();
            return artista;
        }

        public async Task<Artista> UpdateArtistaAsync(Guid id, Artista artista)
        {
            var existingArtista = await _context.Artisti.FindAsync(id);
            if (existingArtista == null) return null;

            existingArtista.Nome = artista.Nome;
            existingArtista.Genere = artista.Genere;
            existingArtista.Biografia = artista.Biografia;

            await _context.SaveChangesAsync();
            return existingArtista;
        }

        public async Task<bool> DeleteArtistaAsync(Guid id)
        {
            var artista = await _context.Artisti.FindAsync(id);
            if (artista == null) return false;

            _context.Artisti.Remove(artista);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
