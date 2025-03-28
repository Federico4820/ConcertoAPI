using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistaController : ControllerBase
    {
        private readonly IArtistaService _artistaService;

        public ArtistaController(IArtistaService artistaService)
        {
            _artistaService = artistaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artista>>> GetArtisti()
        {
            var artisti = await _artistaService.GetArtistiAsync();
            return Ok(artisti);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artista>> GetArtista(Guid id)
        {
            var artista = await _artistaService.GetArtistaByIdAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            return Ok(artista);
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPost]
        public async Task<ActionResult<Artista>> CreateArtista(Artista artista)
        {
            var nuovoArtista = await _artistaService.CreateArtistaAsync(artista);
            return CreatedAtAction(nameof(GetArtista), new { id = nuovoArtista.ArtistaId }, nuovoArtista);
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtista(Guid id, Artista artista)
        {
            var artistaAggiornato = await _artistaService.UpdateArtistaAsync(id, artista);
            if (artistaAggiornato == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtista(Guid id)
        {
            var risultato = await _artistaService.DeleteArtistaAsync(id);
            if (!risultato)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
