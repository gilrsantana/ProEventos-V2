using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;

public class PalestranteService : IPalestranteService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IPalestrantePersist _palestrantePersist;

    public PalestranteService(IPalestrantePersist palestrantePersist, IGeralPersist geralPersist)
    {
        _palestrantePersist = palestrantePersist;
        _geralPersist = geralPersist;
    }

    public async Task<Palestrante?> AddPalestrante(Palestrante model)
    {
        try
        {
            _geralPersist.Add(model);
            if (await _geralPersist.SaveChangesAsync())
            {
                return await _palestrantePersist.GetPalestranteByIdAsync(model.Id, false);
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Palestrante?> UpdatePalestrante(int palestranteId, Palestrante model)
    {
        try
        {
            var palestrante = await _palestrantePersist.GetPalestranteByIdAsync(palestranteId, false);
            if (palestrante == null) return null;
            
            model.Id = palestrante.Id;
            _geralPersist.Update(model);
            if (await _geralPersist.SaveChangesAsync())
            {
                return await _palestrantePersist.GetPalestranteByIdAsync(model.Id, false);
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeletePalestrante(int palestranteId)
    {
        try
        {
            var palestrante = await _palestrantePersist.GetPalestranteByIdAsync(palestranteId, false);
            if (palestrante == null) throw new Exception("Palestrante para delete não encontrado.");
            
            _geralPersist.Delete(palestrante);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirPalestrantes = false)
    {
        try
        {
            return await _palestrantePersist.GetAllPalestrantesAsync(incluirPalestrantes);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false)
    {
        try
        {
            return await _palestrantePersist.GetAllPalestrantesByNomeAsync(nome, incluirEventos);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Palestrante?> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false)
    {
        try
        {
            return await _palestrantePersist.GetPalestranteByIdAsync(palestranteId, incluirEventos);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}