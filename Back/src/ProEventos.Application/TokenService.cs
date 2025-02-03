using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;

namespace ProEventos.Application;

public class TokenService(IConfiguration configuration, UserManager<User> userManager, IMapper mapper)
    : ITokenService
{
    private readonly SymmetricSecurityKey _key = new(
        Encoding
            .UTF8
            .GetBytes(configuration["TokenKey"]
                            ?? throw new InvalidOperationException("TokenKey not found")));

    public async Task<string> CreateToken(UserUpdateDto userUpdateDto)
    {
        var user = mapper.Map<User>(userUpdateDto);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        };

        var roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(2),
            SigningCredentials = creds,
            Issuer = configuration["IssuerKey"],
            
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}