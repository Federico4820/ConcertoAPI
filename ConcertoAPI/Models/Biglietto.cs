using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ConcertoAPI.Models.Auth;

namespace ConcertoAPI.Models
{
    public class Biglietto
    {
        [Key]
        public Guid BigliettoId { get; set; }
        public Guid EventoId { get; set; }

        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser Utente { get; set; }

        public DateTime DataAcquisto { get; set; } = DateTime.Now;
    }
}
