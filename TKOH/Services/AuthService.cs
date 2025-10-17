using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TKOH.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TKOH.Services
{
    public class AuthService
    {
        private readonly ConnectorAPI _api;
        private const string JwtCookieName = "JWT_TOKEN";

        public AuthService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request, HttpContext httpContext)
        {
            var apiResponse = await _api.PostAsync<LoginRequest, ApiResponse<AuthResponse>>("/api/auth/login", request);

            if (apiResponse?.Data != null && !string.IsNullOrEmpty(apiResponse.Data.AccessToken))
            {
                var token = apiResponse.Data.AccessToken;
                var expiresAt = apiResponse.Data.ExpiresAt;

                httpContext.Response.Cookies.Append(JwtCookieName, token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = expiresAt
                });

                var userService = httpContext.RequestServices.GetRequiredService<UserService>();
                var user = await userService.GetCurrentUserAsync(token);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? "unknown_id"),
            new Claim(ClaimTypes.Name, user?.Name ?? request.Email),
            new Claim(ClaimTypes.Email, user?.Email ?? request.Email)
        };

                if (user?.Roles != null)
                {
                    foreach (var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = expiresAt
                });

                return apiResponse.Data;
            }

            return null;
        }

        public async Task<(UserDTO? Response, string? ErrorMessage)> RegisterAsync(RegisterRequest request)
        {
            var res = await _api.PostAsync<RegisterRequest, UserDTO>("/api/auth/register", request);
            if (res == null)
                return (null, "Error al registrar el usuario");

            return (res, null);
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Response.Cookies.Delete(JwtCookieName);
        }

        public string? GetToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies[JwtCookieName];
        }
    }
}