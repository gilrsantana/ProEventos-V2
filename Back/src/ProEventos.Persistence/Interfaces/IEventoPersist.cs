using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface IEventoPersist
{
    Task<Evento[]> GetAllEventosAsync(int userId, bool incluirPalestrantes = false);
    Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool incluirPalestrantes = false);
    Task<Evento?> GetEventoByIdAsync(int userId, int idEvento, bool incluirPalestrantes = false);
}