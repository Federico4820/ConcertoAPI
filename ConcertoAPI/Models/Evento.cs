using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertoAPI.Models
{
    public class Evento
    {
        [Key]
        public Guid EventoId { get; set; }
        [Required]
        [StringLength(50)]
        public required string Titolo { get; set; }
        [Required]
        public required DateTime Data { get; set; }
        [Required]
        public required string Luogo { get; set; }
        public Guid ArtistaId { get; set; }

        [ForeignKey("ArtistaId")]
        public Artista Artista { get; set; }
        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
