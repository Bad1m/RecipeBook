using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Data;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RecipeContext context) : base(context)
        {
        }

        public virtual async Task<User> InsertAsync(User entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await _dbSet.AsNoTracking()
                                    .OrderBy(user => user.Id)
                                    .FirstOrDefaultAsync(user => user.UserName == userName, cancellationToken);

            return user;
        }
    }
}