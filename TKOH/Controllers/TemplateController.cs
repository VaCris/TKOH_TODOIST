using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    [Authorize]
    public class TemplateController : BaseController
    {
        private readonly TemplateService _templateService;
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public TemplateController(TemplateService templateService, AuthService authService, UserService userService)
        {
            _templateService = templateService;
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            var templates = await _templateService.GetAllTemplatesAsync(token);
            return View(templates);
        }

        public async Task<IActionResult> Details(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var template = await _templateService.GetTemplateByIdAsync(id,token);

            if (template == null)
                return NotFound();
            return View(template);
        }

        public async Task<IActionResult> Create()
        {
            var token = _authService.GetToken(HttpContext);
            await PopulateTemplatesViewBag(token);
            return View(new CreateTemplateDTO());
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTemplateDTO model)
        {
            var token = _authService.GetToken(HttpContext);
            var user = await _userService.GetCurrentUserAsync(token);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
                return View(model);
            }

            var sendDto = new CreateTemplateDTO
            {
                Title = model.Title,
                Weekday = model.Weekday,
                Weight = model.Weight,
                Content = model.Content,
                Date = model.Date == default ? DateTime.Now : model.Date,
                UserId = (int)user.Id
            };

            var result = await _templateService.CreateTemplateAsync(sendDto, token);

            if (result != null)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear la plantilla.");
            await PopulateTemplatesViewBag(token);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var template = await _templateService.GetTemplateByIdAsync(id,token);

            if (template == null)
                return NotFound();

            var dto = new UpdateTemplateDTO
            {
                Title = template.Title,
                Content = template.Content,
                IsActive = template.IsActive,
                Weekday = template.Weekday
            };
            await PopulateTemplatesViewBag(token);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, UpdateTemplateDTO model)
        {
            var token = Request.Cookies["JWT_TOKEN"];
            if (ModelState.IsValid)
            {
                var result = await _templateService.UpdateTemplateAsync(id, model, token);

                if (result != null)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "No se pudo actualizar la plantilla.");
            }
            await PopulateTemplatesViewBag(token);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var template = await _templateService.GetTemplateByIdAsync(id,token);

            if (template == null)
                return NotFound();

            return View(template);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var token = _authService.GetToken(HttpContext);
            await _templateService.DeleteTemplateAsync(id,token);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateTemplatesViewBag(string token)
        {
            try
            {
                var templates = await _templateService.GetAllTemplatesAsync(token);

                if (templates != null && templates.Any())
                {
                    ViewBag.Templates = templates
                        .Select(t => new SelectListItem
                        {
                            Value = t.Id.ToString(),
                            Text = t.Title
                        })
                        .ToList();
                }
                else
                {
                    ViewBag.Templates = new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Templates = new List<SelectListItem>();
            }
        }

    }
}
