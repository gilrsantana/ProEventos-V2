using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;

public interface ILoteService
{
    public Task<LoteDto[]?> SaveLote(int eventoId, LoteDto[] models);
    public Task<bool> DeleteLote(int eventoId, int loteId);
    public Task<LoteDto[]?> GetLotesByEventoIdAsync(int eventoId); 
    public Task<LoteDto?> GetLoteByIdsAsync(int eventoId, int loteId);
}