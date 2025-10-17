using Microsoft.AspNetCore.Mvc;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var res = await _authService.LoginAsync(request, HttpContext);

            if (res == null || string.IsNullOrEmpty(res.AccessToken))
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas. Por favor, intente de nuevo.");
                return View(request);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            if (request.Password != request.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Las contraseñas no coinciden.");
                return View(request);
            }

            var (res, error) = await _authService.RegisterAsync(request);

            if (res == null)
            {
                ModelState.AddModelError(string.Empty, error ?? "Error al registrar el usuario. Es posible que el correo ya esté en uso.");
                return View(request);
            }

            var loginRequest = new LoginRequest
            {
                Email = request.Email,
                Password = request.Password
            };

            var loginRes = await _authService.LoginAsync(loginRequest, HttpContext);

            if (loginRes == null || string.IsNullOrEmpty(loginRes.AccessToken))
            {
                TempData["Message"] = "Registro exitoso. Por favor, inicie sesión.";
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _authService.Logout(HttpContext);
            return RedirectToAction("Login");
        }
    }
}
