using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface IEventoService
{
    Task<EventoDto?> AddEvento(int userId, EventoDto model);
    Task<EventoDto?> UpdateEvento(int userId, int eventoId, EventoDto model);
    Task<bool> DeleteEvento(int userId, int eventoId);
    Task<EventoDto[]> GetAllEventosAsync(int userId, bool incluirPalestrantes = false);
    Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool incluirPalestrantes = false);
    Task<EventoDto?> GetEventoByIdAsync(int userId, int eventoId, bool incluirPalestrantes = false);
}