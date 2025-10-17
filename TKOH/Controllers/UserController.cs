using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly AuthService _authService;

        public UsersController(UserService userService, RoleService roleService, AuthService authService)
        {
            _userService = userService;
            _roleService = roleService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            var users = await _userService.GetAllAsync(token);
            return View(users);
        }

        public async Task<IActionResult> Details(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var user = await _userService.GetUserDetailsByIdAsync(id, token);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            var token = _authService.GetToken(HttpContext);
            await PopulateRolesViewBag(token);
            return View(new CreateUserDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO model)
        {
            var token = _authService.GetToken(HttpContext);

            if (model.RoleIds == null || !model.RoleIds.Any())
            {
                ModelState.AddModelError("RoleIds", "Debes seleccionar un rol.");
                await PopulateRolesViewBag(token);
                return View(model);
            }

            var result = await _userService.CreateUserAsync(model, token);

            if (result != null)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear el usuario.");
            await PopulateRolesViewBag(token);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var user = await _userService.GetUserByIdAsync(id, token);
            if (user == null)
            {
                return NotFound();
            }

            var updateUserDto = new UpdateUserDTO
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email,
                CurrentScore = user.CurrentScore,
                Enabled = user.Enabled,
                Locked = user.Locked,
                RoleIds = user.Roles?.Select(r => r.Id).ToList() ?? new List<long>()
            };

            await PopulateRolesViewBag(updateUserDto.RoleIds, token);
            return View(updateUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserDTO userDto)
        {
            var token = Request.Cookies["JWT_TOKEN"];
            if (ModelState.IsValid)
            {
                var updatedUser = await _userService.UpdateUserAsync(userDto.Id, userDto, token);

                if (updatedUser != null)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "No se pudo actualizar al usuario.");
            }

            await PopulateRolesViewBag(userDto.RoleIds, token);
            return View(userDto);
        }


        public async Task<IActionResult> Delete(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var user = await _userService.GetUserByIdAsync(id, token);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var token = _authService.GetToken(HttpContext);
            await _userService.DeleteUserAsync(id, token);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateRolesViewBag(string? token, object? selectedRole = null)
        {
            var rolesList = await _roleService.GetAllSummariesAsync();
            ViewBag.Roles = new SelectList(rolesList, "Id", "Name");
        }

        private async Task PopulateRolesViewBag(List<long>? selectedRoleIds, string? token)
        {
            var rolesList = await _roleService.GetAllSummariesAsync();

            ViewBag.Roles = rolesList.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name,
                Selected = selectedRoleIds != null && selectedRoleIds.Contains(r.Id)
            }).ToList();
        }
    }
}