using ConcertoAPI.Models;

namespace ConcertoAPI.Interfaces
{
    public interface IArtistaService
    {
        Task<IEnumerable<Artista>> GetArtistiAsync();
        Task<Artista> GetArtistaByIdAsync(Guid id);
        Task<Artista> CreateArtistaAsync(Artista artista);
        Task<Artista> UpdateArtistaAsync(Guid id, Artista artista);
        Task<bool> DeleteArtistaAsync(Guid id);
    }

}
