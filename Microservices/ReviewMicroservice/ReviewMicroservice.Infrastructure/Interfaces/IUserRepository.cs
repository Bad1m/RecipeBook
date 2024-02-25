using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task InsertAsync(User user);
    }
}