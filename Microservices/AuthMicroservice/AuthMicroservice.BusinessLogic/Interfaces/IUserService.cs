using AuthMicroservice.BusinessLogic.Dtos;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers(int page = 1, int pageSize = 10);
        Task<UserDto?> GetUserDtoByLoginAsync(string login);
        Task DeleteUserAsync(string login);
    }
}