using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Domain.Settings;

namespace ReviewMicroservice.Infrastructure.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<Review> Reviews;

        public IMongoCollection<Recipe> Recipes;

        public IMongoCollection<User> Users;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            Reviews = _database.GetCollection<Review>("Reviews");
            Recipes = _database.GetCollection<Recipe>("Recipes");
            Users = _database.GetCollection<User>("Users");
        }
    }
}