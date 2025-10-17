using Microsoft.AspNetCore.Mvc;
using TKOH.Services;

namespace TKOH.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly AssignmentService _assignmentService;

        public DashboardController(
            AuthService authService,
            UserService userService,
            RoleService roleService,
            AssignmentService assignmentService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
            _assignmentService = assignmentService;
        }
        public async Task<IActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var currentUser = await _userService.GetCurrentUserAsync(token);
            if (currentUser == null)
            {
                _authService.Logout(HttpContext);
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.CurrentUser = currentUser;
            return View(currentUser);
        }
    }
}