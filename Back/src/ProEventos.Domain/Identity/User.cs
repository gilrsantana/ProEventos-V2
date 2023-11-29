using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Identity;

public class User : IdentityUser<int>
{
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public Titulo Titulo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public Funcao Funcao { get; set; }
    public string ImagemURL { get; set; } = string.Empty;
    public IEnumerable<UserRole> UserRoles { get; set; } = null!;
}