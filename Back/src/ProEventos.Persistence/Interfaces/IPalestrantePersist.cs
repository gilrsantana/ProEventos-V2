using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface IPalestrantePersist
{
    Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false);
    Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false);
    Task<Palestrante?> GetPalestranteByIdAsync(int idPalestrante, bool incluirEventos = false);
}