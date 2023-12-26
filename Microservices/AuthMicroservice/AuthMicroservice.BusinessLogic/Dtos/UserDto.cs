using Microsoft.AspNetCore.Identity;

namespace AuthMicroservice.BusinessLogic.Dtos
{
    public class UserDto : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}