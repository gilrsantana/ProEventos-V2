using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface IEventoPersist
{
    Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false);
    Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false);
    Task<Evento?> GetEventoByIdAsync(int idEvento, bool incluirPalestrantes = false);
}