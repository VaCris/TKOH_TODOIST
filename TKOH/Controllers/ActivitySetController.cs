using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    [Authorize]
    public class ActivitySetController : BaseController
    {
        private readonly ActivitySetService _activitySetService;
        private readonly AuthService _authService;
        public ActivitySetController(ActivitySetService activitySetService, AuthService authService)
        {
            _activitySetService = activitySetService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var token = _authService.GetToken(HttpContext);
            var sets = await _activitySetService.GetAllSetsAsync(token);
            return View(sets);
        }

        [HttpGet]
        public async Task<ActionResult<ActivitySetDTO>> GetSetById(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var set = await _activitySetService.GetSetByIdAsync(id,token);
            if (set == null)
            {
                return NotFound();
            }
            return View(set);
        }

        public async Task<IActionResult> Create()
        {
            return View(new CreateActivitySetDTO());
        }

        [HttpPost]
        public async Task<ActionResult<ActivitySetDTO>> Create(CreateActivitySetDTO createSetDto)
        {
            var token = _authService.GetToken(HttpContext);
            var newSet = await _activitySetService.CreateSetAsync(createSetDto, token);
            if (newSet == null)
            {
                return BadRequest("No se pudo crear el set");
            }
            return CreatedAtAction(nameof(GetSetById), new { id = newSet.Id }, newSet);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var set = await _activitySetService.GetSetByIdAsync(id, token);
            if (set == null)
            {
                return NotFound();
            }
            var updateDto = new UpdateActivitySetDTO
            {
                Title = set.Title,
                Description = set.Description
            };
            return View(updateDto);
        }


        [HttpPut]
        public async Task<ActionResult<ActivitySetDTO>> Edit(long id, UpdateActivitySetDTO updateSetDto)
        {
            var token = _authService.GetToken(HttpContext);
            var updatedSet = await _activitySetService.UpdateSetAsync(id, updateSetDto,token);
            if (updatedSet == null)
            {
                return NotFound();
            }
            return View(updatedSet);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var success = await _activitySetService.DeleteSetAsync(id, token);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ActivitySetDetailDTO>> GetSetDetail(long id)
        {
            var token = _authService.GetToken(HttpContext);
            var detail = await _activitySetService.GetSetDetailByIdAsync(id, token);
            if (detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivitySetSummaryDTO>>> GetSetSummaries()
        {
            var token = _authService.GetToken(HttpContext);
            var summaries = await _activitySetService.GetSetSummariesAsync(token);
            return Ok(summaries);
        }
    }
}
