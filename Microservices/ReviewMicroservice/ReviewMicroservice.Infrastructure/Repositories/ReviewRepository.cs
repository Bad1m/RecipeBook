using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Data;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MongoDBContext _context;

        public ReviewRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            int skip = (pageNumber - 1) * pageSize;
            var pagedReviews = await _context.Reviews.Find(_ => true)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);

            return pagedReviews;
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
        {
            await _context.Reviews.DeleteOneAsync(review => review.Id == id, cancellationToken);
        }

        public async Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Reviews.Find(review => review.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(Review review, CancellationToken cancellationToken)
        {
            await _context.Reviews.InsertOneAsync(review, cancellationToken);
        }

        public async Task UpdateAsync(string id, Review updatedReview, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(review => review.Id, id);

            var updateOptions = new UpdateOptions
            {
                IsUpsert = false
            };

            await _context.Reviews.ReplaceOneAsync(filter, updatedReview, updateOptions, cancellationToken);
        }

        public async Task<List<Review>> GetByRecipeIdAsync(string recipeId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            int skip = (pageNumber - 1) * pageSize;
            var pagedReviews = await _context.Reviews.Find(r => r.RecipeId == recipeId)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);

            return pagedReviews;
        }

        public async Task<bool> IsReviewExistsAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(review => review.Id, id);
            var count = await _context.Reviews.CountDocumentsAsync(filter, new CountOptions(), cancellationToken);

            return count > 0;
        }
    }
}