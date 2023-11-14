using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;

public class LotePersist : ILotePersist
{
    private readonly ProEventosContext _context;

    public LotePersist(ProEventosContext context)
    {
        _context = context;
    }

    public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
    {
        IQueryable<Lote> query = _context.Lotes;
        
        query = query.AsNoTracking()
            .Where(lote => lote.EventoId == eventoId)
            .OrderBy(lote => lote.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Lote?> GetLoteByIdsAsync(int eventoId, int loteId)
    {
        IQueryable<Lote> query = _context.Lotes;
        
        query = query.AsNoTracking()
            .Where(lote => lote.EventoId == eventoId
                           && lote.Id == loteId)
            .OrderBy(lote => lote.Id);

        return await query.FirstOrDefaultAsync();
    }
}