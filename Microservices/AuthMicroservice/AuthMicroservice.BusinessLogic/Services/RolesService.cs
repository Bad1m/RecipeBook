using AuthMicroservice.BusinessLogic.Constants;
using AuthMicroservice.BusinessLogic.Enums;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Entities;   
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
                throw new ArgumentException(ErrorMessages.UserNotFound);
            }

            var roleName = Enum.GetName(typeof(Roles), role);

            if (!Enum.IsDefined(typeof(Roles), role))
            {
                throw new ArgumentException(ErrorMessages.InvalidRoleSpecified);
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.AssignRoleFailed, roleName));
            }
        }
    }
}