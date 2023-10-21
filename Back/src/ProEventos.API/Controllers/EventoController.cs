using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get(bool incluirPalestrantes = false)
    {
        try
        {
            var eventos = await _eventoService.GetAllEventosAsync(incluirPalestrantes);
            if (eventos.Length == 0) return NotFound("Nenhum evento encontrado.");
            
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
        }
    }

    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetById(int id, bool incluirPalestrantes = false)
    {
        try
        {
            var evento = await _eventoService.GetEventoByIdAsync(id, incluirPalestrantes);
            if (evento == null) return NotFound("Evento por id encontrado.");
            
            return Ok(evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar evento por id. Erro: {e.Message}");
        }
    }
    
    [HttpGet("GetByTema/{tema}")]
    public async Task<IActionResult> GetByTema(string tema, bool incluirPalestrantes = false)
    {
        try
        {
            var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, incluirPalestrantes);
            if (eventos.Length == 0) return NotFound("Eventos por tema não encontrados.");
            
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar evento por tema. Erro: {e.Message}");
        }
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post(Evento model)
    {
        try
        {
            var evento = await _eventoService.AddEvento(model);
            return evento == null 
                ? BadRequest("Erro ao tentar adicionar evento.") 
                : StatusCode(StatusCodes.Status201Created, evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar evento. Erro: {e.Message}");
        }
    }

    [HttpPut("Put/{id}")]
    public async Task<IActionResult> Put(int id, Evento model)
    {
        try
        {
            var evento = await _eventoService.UpdateEvento(id, model);
            return evento == null 
                ? BadRequest("Erro ao tentar atualizar evento.") 
                : StatusCode(StatusCodes.Status200OK, evento);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar evento. Erro: {e.Message}");
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _eventoService.DeleteEvento(id);
            return result == false 
                ? BadRequest("Erro ao tentar remover evento.") 
                : StatusCode(StatusCodes.Status200OK, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar remover evento. Erro: {e.Message}");
        }
    }
    
}
