using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TKOH.DTOs;
using TKOH.Services;
using System.Threading.Tasks;

namespace TKOH.Controllers
{
    [Authorize]
    public class ScoreHistoryController : BaseController
    {
        private readonly ScoreHistoryService _scoreHistoryService;
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public ScoreHistoryController(ScoreHistoryService scoreHistoryService, UserService userService, AuthService authService)
        {
            _scoreHistoryService = scoreHistoryService;
            _userService = userService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            var history = await _scoreHistoryService.GetScoreHistoryAsync(token);
            return View(history);
        }

        public async Task<IActionResult> Details(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var detail = await _scoreHistoryService.GetHistoryEntryDetailByIdAsync(id, token);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        public async Task<IActionResult> Create()
        {
            var token = _authService.GetToken(HttpContext);
            await PopulateUsersViewBag(token);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateScoreHistoryDTO historyDto)
        {
            var token = _authService.GetToken(HttpContext);
            if (ModelState.IsValid)
            {
                var newEntry = await _scoreHistoryService.CreateHistoryEntryAsync(historyDto, token);
                if (newEntry != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo crear la entrada.");
            }
            await PopulateUsersViewBag(token);
            return View(historyDto);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var entry = await _scoreHistoryService.GetHistoryEntryByIdAsync(id, token);
            if (entry == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateScoreHistoryDTO
            {
                Score = entry.Score,
                Reason = entry.Reason
            };
            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, UpdateScoreHistoryDTO historyDto)
        {
            var token = _authService.GetToken(HttpContext);
            if (ModelState.IsValid)
            {
                var updatedEntry = await _scoreHistoryService.UpdateHistoryEntryAsync(id, historyDto,token);
                if (updatedEntry != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo actualizar la entrada.");
            }
            return View(historyDto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var entry = await _scoreHistoryService.GetHistoryEntryByIdAsync(id, token);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var token = _authService.GetToken(HttpContext);
            await _scoreHistoryService.DeleteHistoryEntryAsync(id, token);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateUsersViewBag(string? token)
        {
            var users = await _userService.GetAllAsync(token);
            ViewBag.Users = new SelectList(users, "Id", "Name");
        }
    }
}