using ProEventos.Domain.Identity;

namespace ProEventos.Domain;

public class Palestrante
{
    public int Id { get; set; }
    public string MiniCurriculo { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<RedeSocial>? RedesSociais { get; set; }
    public ICollection<PalestranteEvento>? PalestrantesEventos { get; set; }
}
