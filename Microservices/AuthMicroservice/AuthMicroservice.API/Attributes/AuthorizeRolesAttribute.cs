using AuthMicroservice.BusinessLogic.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AuthMicroservice.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Roles[] roles)
        {
            Roles = string.Join(",", roles.Select(role => role.ToString()));
        }
    }
}