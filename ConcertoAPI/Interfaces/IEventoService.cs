using ConcertoAPI.Models;

namespace ConcertoAPI.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetEventiAsync();
        Task<Evento> GetEventoByIdAsync(Guid id);
        Task<Evento> CreateEventoAsync(Evento evento);
        Task<Evento> UpdateEventoAsync(Guid id, Evento evento);
        Task<bool> DeleteEventoAsync(Guid id);
    }

}
