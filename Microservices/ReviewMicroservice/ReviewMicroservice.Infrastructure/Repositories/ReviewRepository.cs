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

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Find(_ => true).ToListAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _context.Reviews.DeleteOneAsync(review => review.Id == id);
        }

        public async Task<Review> GetByIdAsync(string id)
        {
            return await _context.Reviews.Find(review => review.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Review review)
        {
            await _context.Reviews.InsertOneAsync(review);
        }

        public async Task UpdateAsync(string id, Review updatedReview)
        {
            await _context.Reviews.ReplaceOneAsync(review => review.Id == id, updatedReview);
        }

        public async Task<List<Review>> GetByRecipeIdAsync(string recipeId)
        {
            return await _context.Reviews.Find(r => r.RecipeId == recipeId).ToListAsync();
        }
    }
}