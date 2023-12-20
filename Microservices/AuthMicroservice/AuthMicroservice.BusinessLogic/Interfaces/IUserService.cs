using AuthMicroservice.BusinessLogic.Dtos;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        Task<UserDto?> GetUserByLoginAsync(string login);
        Task DeleteUserAsync(string login);
    }
}