namespace ConcertoAPI.DTOs.Bigleitti
{
    public class AcquistoBigliettoDto
    {
        public Guid EventoId { get; set; }
        public int Quantita { get; set; } = 1;
    }
}
