using System.ComponentModel.DataAnnotations;

namespace ConcertoAPI.Models
{
    public class Artista
    {
        [Key]
        public Guid ArtistaId { get; set; }
        [Required]
        [StringLength(25)]
        public required string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public required string Genere { get; set; }
        [Required]
        [StringLength(200)]
        public required string Biografia { get; set; }
        public ICollection<Evento>? Eventi { get; set; }
    }
}
