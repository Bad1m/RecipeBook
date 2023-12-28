using MongoDB.Driver;
using ReviewMicroservice.Domain.Entities;

namespace ReviewMicroservice.Infrastructure.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("Reviews");
    }
}