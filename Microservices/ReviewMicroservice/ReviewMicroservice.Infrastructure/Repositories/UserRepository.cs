using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Data;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _context;

        public UserRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users.Find(user => user.UserName == userName).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }
    }
}