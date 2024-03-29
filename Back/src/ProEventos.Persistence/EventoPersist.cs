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
    
    public async Task<Evento[]> GetAllEventosAsync(int userId, bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.AsNoTracking()
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)!
                .ThenInclude(pe => pe.Palestrante);
        }

        query = query
            .Where(e => e.UserId == userId)
            .OrderBy(e => e.Id);
        
        return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.AsNoTracking()
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)!
                .ThenInclude(pe => pe.Palestrante);
        }
        
        query = query
            .Where(e => e.Tema.ToLower().Contains(tema.ToLower()) 
                        && e.UserId == userId)
            .OrderBy(e => e.Id);
        
        return await query.ToArrayAsync();
    }
    
    public async Task<Evento?> GetEventoByIdAsync(int userId, int idEvento, bool incluirPalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.AsNoTracking()
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

        if (incluirPalestrantes)
        {
            query = query
                .Include(e => e.PalestrantesEventos)!
                .ThenInclude(pe => pe.Palestrante);
        }
        
        query = query
            .Where(e => e.Id == idEvento && e.UserId == userId)
            .OrderBy(e => e.Id);
        
        return await query.FirstOrDefaultAsync();
    }
}