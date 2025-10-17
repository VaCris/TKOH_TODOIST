using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    public class AssignmentController : BaseController
    {
        private readonly AssignmentService _assignmentService;
        private readonly TemplateService _templateService;
        private readonly UserService _userService;
        private readonly AuthService _authService;
        public AssignmentController(AssignmentService assignmentService, TemplateService templateService, UserService userService, AuthService authService)
        {
            _assignmentService = assignmentService;
            _templateService = templateService;
            _userService = userService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            var assignments = await _assignmentService.GetAllAssignmentsAsync(token);
            return View(assignments);
        }

        public async Task<IActionResult> Details(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id,token);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        public async Task<IActionResult> Create()
        {
            var token = _authService.GetToken(HttpContext);
            await PopulateDropdowns(token);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentDTO assignmentDto)
        {
            var token = _authService.GetToken(HttpContext);
            if (ModelState.IsValid)
            {
                var newAssignment = await _assignmentService.CreateAssignmentAsync(assignmentDto,token);
                if (newAssignment != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo crear la asignación.");
            }
            await PopulateDropdowns(token);
            return View(assignmentDto);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id, token);
            if (assignment == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, UpdateAssignmentDTO assignmentDto)
        {
            var token = _authService.GetToken(HttpContext);
            if (ModelState.IsValid)
            {
                var updatedAssignment = await _assignmentService.UpdateAssignmentAsync(id, assignmentDto,token);
                if (updatedAssignment != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo actualizar la asignación.");
            }
            return View(assignmentDto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var assignment = await _assignmentService.GetAssignmentByIdAsync(id, token);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var token = _authService.GetToken(HttpContext);
            await _assignmentService.DeleteAssignmentAsync(id, token);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateDropdowns(string? token)
        {
            var templates = await _templateService.GetAllTemplatesAsync(token);
            var users = await _userService.GetAllAsync(token);

            ViewBag.Templates = new SelectList(templates, "Id", "Title");
            ViewBag.Users = new SelectList(users, "Id", "Name");
        }
    }
}