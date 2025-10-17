using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TKOH.DTOs;
using TKOH.Services;

namespace TKOH.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesApiController : BaseController
    {
        private readonly RoleService _roleService;

        public RolesApiController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("summaries")]
        public async Task<ActionResult<List<RoleSummaryDTO>>> GetAllSummaries()
        {
            var summaries = await _roleService.GetAllSummariesAsync();
            return Ok(summaries);
        }
    }
}