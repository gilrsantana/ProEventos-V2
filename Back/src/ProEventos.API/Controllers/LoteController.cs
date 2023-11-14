using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoteController : ControllerBase
{
    private readonly ILoteService _loteService;

    public LoteController(ILoteService eventoService)
    {
        _loteService = eventoService;
    }

    [HttpGet("Get/{eventoId}")]
    public async Task<IActionResult> Get(int eventoId)
    {
        try
        {
            var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
            if (lotes is null || !lotes.Any()) return NoContent();

            return Ok(lotes);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar lotes. Erro: {e.Message}");
        }
    }

    [HttpPut("Put/{eventoId}")]
    public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
    {
        try
        {
            var lotes = await _loteService.SaveLote(eventoId, models);
            return lotes == null 
                ? BadRequest("Erro ao tentar atualizar lotes.") 
                : StatusCode(StatusCodes.Status200OK, lotes);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar lotes. Erro: {e.Message}");
        }
    }

    [HttpDelete("Delete/{eventoId}/{loteId}")]
    public async Task<IActionResult> Delete(int eventoId, int loteId)
    {
        try
        {
            var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) return NoContent();
            
            var result = await _loteService.DeleteLote(lote.EventoId, lote.Id);
            return result == false 
                ? BadRequest("Erro ao tentar remover lote.") 
                : StatusCode(StatusCodes.Status200OK, new { message = "Lote removido." });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar remover lote. Erro: {e.Message}");
        }
    }
    
}
