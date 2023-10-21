using ProEventos.Domain;

namespace ProEventos.Application.Interfaces;

public interface IPalestranteService
{
    Task<Palestrante?> AddPalestrante(Palestrante model);
    Task<Palestrante?> UpdatePalestrante(int palestranteId, Palestrante model);
    Task<bool> DeletePalestrante(int palestranteId);
    Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirPalestrantes = false);
    Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false);
    Task<Palestrante?> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false);
}