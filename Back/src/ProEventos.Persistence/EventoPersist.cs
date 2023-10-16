using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;

public class EventoPersist : IEventoPersist
{
    private readonly ProEventosContext _context;

    public EventoPersist(ProEventosContext context)
    {
        _context = context;
    }
    
    public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
        }
        
        query = query.OrderBy(e => e.Id);
        
        return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
        }
        
        query = query
            .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
            .OrderBy(e => e.Id);
        
        return await query.ToArrayAsync();
    }
    
    public async Task<Evento?> GetEventoByIdAsync(int idEvento, bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
        }
        
        query = query
            .Where(e => e.Id == idEvento)
            .OrderBy(e => e.Id);
        
        return await query.FirstOrDefaultAsync();
    }
}