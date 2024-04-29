namespace ProEventos.Application.Dtos;

public class UserUpdateDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Funcao { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

}