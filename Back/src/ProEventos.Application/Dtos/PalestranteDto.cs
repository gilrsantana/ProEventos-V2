using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class PalestranteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string MiniCurriculo { get; set; } = string.Empty;
    public string ImagemURL { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<RedeSocialDto>? RedesSociais { get; set; }
    public IEnumerable<EventoDto>? Eventos { get; set; }
}