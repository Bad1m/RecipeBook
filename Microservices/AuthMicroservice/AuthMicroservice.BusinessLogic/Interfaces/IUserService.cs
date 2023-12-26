using AuthMicroservice.BusinessLogic.Dtos;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(int page = 1, int pageSize = 10);
        Task<UserDto?> GetUserDtoByLoginAsync(string login);
        Task DeleteUserAsync(string login);
    }
}