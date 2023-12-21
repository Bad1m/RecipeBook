using AuthMicroservice.BusinessLogic.Enums;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task AssignRoleToUserAsync(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var roleName = Enum.GetName(typeof(Roles), role);
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to assign role '{roleName}' to user.");
            }
        }
    }
}