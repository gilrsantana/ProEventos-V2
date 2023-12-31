using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface IEventoService
{
    Task<EventoDto?> AddEvento(EventoDto model);
    Task<EventoDto?> UpdateEvento(int eventoId, EventoDto model);
    Task<bool> DeleteEvento(int eventoId);
    Task<EventoDto[]> GetAllEventosAsync(bool incluirPalestrantes = false);
    Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false);
    Task<EventoDto?> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false);
}