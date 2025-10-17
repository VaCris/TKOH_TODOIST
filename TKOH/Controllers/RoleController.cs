using Microsoft.AspNetCore.Mvc;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO roleDto)
        {
            if (ModelState.IsValid)
            {
                var newRole = await _roleService.CreateAsync(roleDto);
                if (newRole != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo crear el rol. El nombre puede ya existir.");
            }
            return View(roleDto);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var updateDto = new UpdateRoleDTO { Name = role.Name };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, UpdateRoleDTO roleDto)
        {
            if (ModelState.IsValid)
            {
                var updatedRole = await _roleService.UpdateRoleAsync(id, roleDto);
                if (updatedRole != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el rol.");
            }
            return View(roleDto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _roleService.DeleteRoleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}