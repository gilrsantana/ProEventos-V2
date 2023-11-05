using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    private readonly IMapper _mapper;

    public EventoService(IGeralPersist geralPersist, 
                         IEventoPersist eventoPersist,
                         IMapper mapper)
    {
        _geralPersist = geralPersist;
        _eventoPersist = eventoPersist;
        _mapper = mapper;
    }

    public async Task<EventoDto?> AddEvento(EventoDto model)
    {
        try
        {
            var evento = _mapper.Map<Evento>(model);

            _geralPersist.Add(evento);

            if (!await _geralPersist.SaveChangesAsync()) return null;
            var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(evento.Id);
            return _mapper.Map<EventoDto>(eventoRetorno);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventoDto?> UpdateEvento(int eventoId, EventoDto model)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);
            if (evento is null) return null;

            var eventoAtualizado = _mapper.Map<Evento>(model);
            eventoAtualizado.Id = evento.Id;

            _geralPersist.Update(eventoAtualizado);

            if (await _geralPersist.SaveChangesAsync())
            {
                var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(eventoAtualizado.Id);
                return _mapper.Map<EventoDto>(eventoRetorno);
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
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");
            
            _geralPersist.Delete(evento);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventoDto[]> GetAllEventosAsync(bool incluirPalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosAsync(incluirPalestrantes);
            var resultado = _mapper.Map<EventoDto[]>(eventos);
            return resultado;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, incluirPalestrantes);
            
            return _mapper.Map<EventoDto[]>(eventos);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventoDto?> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, incluirPalestrantes);
            return evento is null ? null : _mapper.Map<EventoDto>(evento);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}