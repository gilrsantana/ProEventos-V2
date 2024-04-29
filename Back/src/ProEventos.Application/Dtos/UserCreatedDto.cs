namespace ProEventos.Application.Dtos;

public class UserCreatedDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}