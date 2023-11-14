using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;

public interface ILotePersist
{
    /// <summary>
    /// Método que retornará uma lista de lotes por eventoId.
    /// </summary>
    /// <param name="eventoId"></param>
    /// <returns>Uma coleção de lotes</returns>
    Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

    /// <summary>
    /// Método que retornará um lote com base no loteId.
    /// </summary>
    /// <param name="eventoId">Código do evento que contém os lotes</param>
    /// <param name="loteId">Código chave do lote</param>
    /// <returns>Apenas um lote</returns>
    Task<Lote?> GetLoteByIdsAsync(int eventoId, int loteId);
}