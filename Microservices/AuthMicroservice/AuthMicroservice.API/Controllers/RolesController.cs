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

        [HttpPost("{userId}")]
        public async Task<IActionResult> AssignAdminRoleToUserAsync(string userId)
        {
            await _rolesService.AssignRoleToUserAsync(userId, Roles.Admin);

            return NoContent();
        }
    }
}