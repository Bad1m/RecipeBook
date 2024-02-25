using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> InsertAsync(User entity);
        Task SaveChangesAsync();
        Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}