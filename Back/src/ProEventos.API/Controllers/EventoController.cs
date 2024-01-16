using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.API.Helpers;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EventoController(IEventoService eventoService, IUtil util)
    : ControllerBase
{
    private readonly string _destino = "Images";
    
    [HttpGet("Get")]
    public async Task<IActionResult> Get(bool incluirPalestrantes = false)
    {
        try
        {
            var eventos = await eventoService.GetAllEventosAsync(User.GetUserId(),incluirPalestrantes);
            if (eventos.Length == 0) return NoContent();

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
            var evento = await eventoService.GetEventoByIdAsync(User.GetUserId(), id, incluirPalestrantes);
            if (evento == null) return NoContent();
            
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
            var eventos = await eventoService.GetAllEventosByTemaAsync(User.GetUserId(), tema, incluirPalestrantes);
            if (eventos.Length == 0) return NoContent();
            
            return Ok(eventos);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar evento por tema. Erro: {e.Message}");
        }
    }
    
    [HttpPost("upload-image/{eventoId}")]
    public async Task<IActionResult> UploadImage(int eventoId)
    {
        try
        {
            var evento = await eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId);
            if (evento == null) return NoContent();
            
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                util.DeleteImage(evento.ImagemURL, _destino);
                evento.ImagemURL = await util.SaveImage(file, _destino);
            }
            var eventoRetorno = await eventoService.UpdateEvento( User.GetUserId(),eventoId, evento);
            return Ok(eventoRetorno);
            
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar imagem. Erro: {e.Message}");
        }
    }
    
    [HttpPost("Post")]
    public async Task<IActionResult> Post(EventoDto model)
    {
        try
        {
            var evento = await eventoService.AddEvento(User.GetUserId(), model);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
        try
        {
            var evento = await eventoService.UpdateEvento(User.GetUserId(), id, model);
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
            var evento = await eventoService.GetEventoByIdAsync(User.GetUserId(), id);
            if (evento == null) return NoContent();
            
            var result = await eventoService.DeleteEvento(User.GetUserId(), id);
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
