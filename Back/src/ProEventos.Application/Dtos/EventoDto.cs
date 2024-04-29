using System.ComponentModel.DataAnnotations;
using ProEventos.Domain;

namespace ProEventos.Application.Dtos;

public class EventoDto
{
    public int Id { get; set; }
    public string Local { get; set; } = string.Empty;
    public string DataEvento { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório."),
     StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
    public string Tema { get; set; } = string.Empty;

    [Display(Name = "Qtd Pessoas"),
     Range(1, 120000, ErrorMessage = "{0} deve ser entre 1 e 120.000.")]
    public int QtdPessoas { get; set; }

    [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
    public string ImagemURL { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório."),
     Phone(ErrorMessage = "{0} está em formato inválido.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} é obrigatório."),
     Display(Name = "e-mail"),
     EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
    public string Email { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    public UserDto? UserDto { get; set; }
    public IEnumerable<LoteDto> Lotes { get; set; } = new List<LoteDto>();
    public IEnumerable<RedeSocialDto> RedesSociais { get; set; } = new List<RedeSocialDto>();
    public IEnumerable<PalestranteDto> Palestrantes { get; set; } = new List<PalestranteDto>();

}