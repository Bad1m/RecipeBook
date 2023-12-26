using AuthMicroservice.BusinessLogic.Enums;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IRolesService
    {
        Task AssignRoleToUserAsync(string userId, Roles role);
    }
}