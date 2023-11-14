using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class LoteService : ILoteService
{
    private readonly ILotePersist _lotePersist;
    private readonly IGeralPersist _geralPersist;
    private readonly IMapper _mapper;

    public LoteService(ILotePersist lotePersist, IGeralPersist geralPersist, IMapper mapper)
    {
        _lotePersist = lotePersist;
        _geralPersist = geralPersist;
        _mapper = mapper;
    }

    public async  Task<LoteDto[]?> GetLotesByEventoIdAsync(int eventoId)
    {
        try
        {
            var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);

            if (lotes == null) return null;

            var resultado = _mapper.Map<LoteDto[]>(lotes);

            return resultado;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<LoteDto?> GetLoteByIdsAsync(int eventoId, int loteId)
    {
        try
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);

            if (lote == null) return null;

            var resultado = _mapper.Map<LoteDto>(lote);

            return resultado;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<LoteDto[]?> SaveLote(int eventoId, LoteDto[] models)
    {
        var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
        if(lotes == null) return null;

        foreach (var model in models)
        {
            if(model.Id == 0)
            {
                await AddLote(eventoId, model);
            }
            else
            {
                await UpdateLote(eventoId, lotes, model);
            }
        }

        var loteRetorno = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
        return _mapper.Map<LoteDto[]>(loteRetorno);
    }

    private async Task AddLote(int eventoId, LoteDto model)
    {
        try
        {
            var lote = _mapper.Map<Lote>(model);
            lote.EventoId = eventoId;

            _geralPersist.Add<Lote>(lote);

            await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task UpdateLote(int eventoId, Lote[] lotes, LoteDto model)
    {
        try
        {
            var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
            if(lote == null) throw new Exception("Lote não encontrado.");
            model.EventoId = eventoId;

            _mapper.Map(model, lote);

            _geralPersist.Update<Lote>(lote);

            await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
           throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteLote(int eventoId, int loteId)
    {
        try
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote para delete não encontrado.");

            _geralPersist.Delete<Lote>(lote);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}