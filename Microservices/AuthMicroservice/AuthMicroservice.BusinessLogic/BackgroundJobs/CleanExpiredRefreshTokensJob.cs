using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.DataAccess.Data;

namespace AuthMicroservice.BusinessLogic.BackgroundJobs
{
    public class CleanExpiredRefreshTokensJob : ICleanExpiredRefreshTokensJob
    {
        private readonly AuthContext _dbContext;

        public CleanExpiredRefreshTokensJob(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Execute()
        {
            var expiredTokens = _dbContext.Users
                .Where(u => u.RefreshTokenExpiryTime <= DateTime.UtcNow)
                .ToList();

            _dbContext.Users.RemoveRange(expiredTokens);
            _dbContext.SaveChanges();
        }
    }
}