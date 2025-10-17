using Microsoft.AspNetCore.Http;
using TKOH.DTOs;
using TKOH.Extensions;

public static class HttpContextExtensions
{
    public static UserDTO? GetCurrentUser(this HttpContext context)
    {
        return context.Session.GetObject<UserDTO>("CurrentUser");
    }
}
