using ConcertoAPI.Models;

namespace ConcertoAPI.Interfaces
{
    public interface IBigliettoService
    {
        Task<IEnumerable<Biglietto>> GetBigliettiByUserAsync(string userId);
        Task<Biglietto> AcquistaBigliettoAsync(Guid eventoId, string userId);
        Task<IEnumerable<Biglietto>> GetAllBigliettiAsync();
    }

}
