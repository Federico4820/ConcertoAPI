using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventi()
        {
            var eventi = await _eventoService.GetEventiAsync();
            return Ok(eventi);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(Guid id)
        {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPost]
        public async Task<ActionResult<Evento>> CreateEvento(Evento evento)
        {
            var nuovoEvento = await _eventoService.CreateEventoAsync(evento);
            return CreatedAtAction(nameof(GetEvento), new { id = nuovoEvento.EventoId }, nuovoEvento);
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(Guid id, Evento evento)
        {
            var eventoAggiornato = await _eventoService.UpdateEventoAsync(id, evento);
            if (eventoAggiornato == null) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(Guid id)
        {
            var risultato = await _eventoService.DeleteEventoAsync(id);
            if (!risultato) return NotFound();
            return NoContent();
        }
    }
}
