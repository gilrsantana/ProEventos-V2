using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController(
    ILogger<AccountController> logger,
    IAccountService accountService,
    ITokenService tokenService)
    : ControllerBase
{
    private readonly ILogger<AccountController> _logger = logger;

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var username = User.GetUserName();
            var user = await accountService.GetUserByUserNameAsync(username);
            return user != null
                ? Ok(user)
                : NotFound("Usuário não encontrado!");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
        }
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        try
        {
            var user = await accountService.GetUserByUserNameAsync(userLoginDto.Username);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos!");

            var result = await accountService.CheckUserPasswordAsync(user, userLoginDto.Password);

            if (result is { Succeeded: false })
                return Unauthorized("Usuário ou senha inválidos!");

            var userToReturn = new
            {
                Username = user.Username,
                PrimeiroNome = user.PrimeiroNome,
                Token = await tokenService.CreateToken(user)
            };

            return Ok(userToReturn);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar realizar login. Erro: {ex.Message}");
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        try
        {
            if (await accountService.UserExists(userDto.Username))
                return BadRequest("Usuário já cadastrado!");

            var user = await accountService.CreateAccountAsync(userDto);

            return user != null
                ? Ok(user)
                : BadRequest("Erro ao tentar cadastrar usuário!");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        try
        {
            var user = await accountService.GetUserByUserNameAsync(User.GetUserName());

            if (user == null)
                return Unauthorized("Usuário inválido!");

            var userReturn = await accountService.UpdateAccountAsync(userUpdateDto);

            return userReturn != null
                ? Ok(userReturn)
                : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
        }
    }

}