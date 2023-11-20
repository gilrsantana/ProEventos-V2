using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;

public class PalestrantePersist : IPalestrantePersist
{
    private readonly ProEventosContext _context;

    public PalestrantePersist(ProEventosContext context)
    {
        _context = context;
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
            .Include(p => p.RedesSociais);

        if (incluirEventos)
        {
            query = query
                .Include(p => p.PalestrantesEventos)!
                .ThenInclude(pe => pe.Evento);
        }
        
        query = query.OrderBy(p => p.Id);
        
        return await query.ToArrayAsync();
    }
    
    public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
            .Include(p => p.RedesSociais);

        if (incluirEventos)
        {
            query = query
                .Include(p => p.PalestrantesEventos)!
                .ThenInclude(pe => pe.Evento);
        }
        
        query = query
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
            .OrderBy(p => p.Id);
        
        return await query.ToArrayAsync();
    }
    
    public async Task<Palestrante?> GetPalestranteByIdAsync(int idPalestrante, bool incluirEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
            .Include(p => p.RedesSociais);

        if (incluirEventos)
        {
            query = query
                .Include(p => p.PalestrantesEventos)!
                .ThenInclude(pe => pe.Evento);
        }
        
        query = query
            .Where(p =>  p.Id == idPalestrante)
            .OrderBy(p => p.Id);
        
        return (await query.FirstOrDefaultAsync())!;
    }
}