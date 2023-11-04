using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class LoteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string DataInicio { get; set; } = string.Empty;
    public string DataFim { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public int EventoId { get; set; }
    public EventoDto? Evento { get; set; }
}