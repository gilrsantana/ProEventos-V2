namespace ProEventos.Domain;

public class PalestranteEvento
{
    public int IdPalestrante { get; set; }
    public Palestrante Palestrante { get; set; }
    public int IdEvento { get; set; }
    public Evento Evento { get; set; }
}
