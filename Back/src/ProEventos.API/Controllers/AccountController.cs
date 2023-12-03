using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService accountService;
    private readonly ITokenService tokenService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService, ITokenService tokenService)
    {
        _logger = logger;
        this.accountService = accountService;
        this.tokenService = tokenService;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            

            return Ok();
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
        }
    }
}