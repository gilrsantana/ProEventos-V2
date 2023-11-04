using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class RedeSocialDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
    public int? EventoId { get; set; }
    public EventoDto? Evento { get; set; }
    public int? PalestranteId { get; set; }
    public PalestranteDto? Palestrante { get; set; }
}