using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersApiController : BaseController
    {
        private readonly UserService _userService;

        public UsersApiController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<UserDTO>>> SearchUsers([FromQuery] string q, string? token)
        {
            var users = await _userService.SearchUsersAsync(q, token);
            return Ok(users);
        }

        [HttpGet("by-role/{role}")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersByRole(string role , string? token)
        {
            var users = await _userService.GetUsersByRoleAsync(role, token);
            return Ok(users);
        }

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> ChangePassword(long id, ChangePasswordDTO changePasswordDto, string? token)
        {
            var success = await _userService.ChangePasswordAsync(id, changePasswordDto,token);
            if (!success)
            {
                return BadRequest("La contraseña actual es incorrecta o la nueva contraseña no es válida");
            }
            return NoContent();
        }

        [HttpPost("{id}/score-adjustments")]
        public async Task<IActionResult> AdjustScore(long id, ScoreAdjustmentDTO scoreAdjustmentDto,string? token)
        {
            var success = await _userService.AdjustScoreAsync(id, scoreAdjustmentDto,token);
            if (!success)
            {
                return BadRequest("No se pudo ajustar el puntaje");
            }
            return Ok();
        }

        [HttpPost("{id}/lock")]
        public async Task<IActionResult> LockUser(long id,string? token)
        {
            var success = await _userService.LockUserAsync(id,token);
            if (!success)
            {
                return NotFound();
            }
            return Ok("Usuario bloqueado exitosamente");
        }

        [HttpPost("{id}/unlock")]
        public async Task<IActionResult> UnlockUser(long id, string? token)
        {
            var success = await _userService.UnlockUserAsync(id, token);
            if (!success)
            {
                return NotFound();
            }
            return Ok("Usuario desbloqueado exitosamente");
        }
    }
}
