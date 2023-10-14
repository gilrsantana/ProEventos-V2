using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventoController : ControllerBase
{
    // private readonly DataContext _context;

    // public EventoController(DataContext context)
    // {
    //     _context = context;
    // }

    // [HttpGet("Get")]
    // public IEnumerable<Evento> Get()
    // {
    //     return _context.Eventos.OrderBy(e => e.Tema).ToList();
    // }

    // [HttpGet("GetById/{id}")]
    // public Evento? GetById(int id)
    // {
    //     return _context.Eventos.FirstOrDefault(e => e.EventoId == id);
    // }

    // [HttpPost("Post")]
    // public string Post(Evento evento)
    // {
    //     evento.EventoId = _context.Eventos.Count() + 1;
    //     _context.Eventos.Add(evento);
        
    //     return _context.SaveChanges().ToString();
    // }

    // [HttpPut("Put/{id}")]
    // public string Put(int id, Evento evento)
    // {
    //     if (id != evento.EventoId)
    //         return "Evento não encontrado";

    //     _context.Eventos.Update(evento);
    //     return _context.SaveChanges().ToString();
    // }

    // [HttpDelete("Delete/{id}")]
    // public string Delete(int id)
    // {
    //     var evento = _context.Eventos.FirstOrDefault(e => e.EventoId == id);
    //     if (evento == null)
    //         return "Evento não encontrado";

    //     _context.Eventos.Remove(evento);
    //     return _context.SaveChanges().ToString();
    // }
    
}
