using System.Security.Claims;

namespace ProEventos.API.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimTypes.Name)?.Value;
        return value ?? "";
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return value != null ? int.Parse(value) : 0;
    }
}