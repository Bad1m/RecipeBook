using AuthMicroservice.BusinessLogic.Enums;
using AuthMicroservice.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost("assign-role/{userId}")]
        public async Task<IActionResult> AssignRoleToUserAsync(string userId, Roles role)
        {
            await _rolesService.AssignRoleToUserAsync(userId, role);

            return NoContent();
        }
    }
}