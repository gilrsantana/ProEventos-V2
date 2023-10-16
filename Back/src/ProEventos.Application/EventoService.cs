using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;

    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
        _geralPersist = geralPersist;
        _eventoPersist = eventoPersist;
    }

    public async Task<Evento?> AddEvento(Evento model)
    {
        try
        {
            _geralPersist.Add(model);
            if (await _geralPersist.SaveChangesAsync())
            {
                return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Evento?> UpdateEvento(int eventoId, Evento model)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (evento == null) return null;

            model.Id = evento.Id;
            _geralPersist.Update(model);
            if (await _geralPersist.SaveChangesAsync())
            {
                return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");
            
            _geralPersist.Update(evento);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
    {
        throw new NotImplementedException();
    }

    public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
    {
        throw new NotImplementedException();
    }

    public Task<Evento?> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
    {
        throw new NotImplementedException();
    }
}