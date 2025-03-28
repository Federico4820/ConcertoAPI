using ConcertoAPI.DTOs.Bigleitti;
using ConcertoAPI.Interfaces;
using ConcertoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConcertoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BigliettoController : ControllerBase
    {
        private readonly IBigliettoService _bigliettoService;

        public BigliettoController(IBigliettoService bigliettoService)
        {
            _bigliettoService = bigliettoService;
        }

        [Authorize]
        [HttpGet("miei")]
        public async Task<ActionResult<IEnumerable<Biglietto>>> GetBigliettiUtente()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var biglietti = await _bigliettoService.GetBigliettiByUserAsync(userId);
            return Ok(biglietti);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Biglietto>> AcquistaBiglietto([FromBody] AcquistoBigliettoDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            if (userId == null) return Unauthorized();

            var biglietto = await _bigliettoService.AcquistaBigliettoAsync(dto.EventoId, userId);
            return CreatedAtAction(nameof(GetBigliettiUtente), new { id = biglietto.BigliettoId }, biglietto);
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biglietto>>> GetAllBiglietti()
        {
            var biglietti = await _bigliettoService.GetAllBigliettiAsync();
            return Ok(biglietti);
        }
    }
}
