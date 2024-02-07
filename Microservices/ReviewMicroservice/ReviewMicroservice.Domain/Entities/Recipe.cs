using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReviewMicroservice.Domain.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("RecipeId")]
        public int RecipeId { get; set; }
        [BsonElement("Dish")]
        public string Dish { get; set; }
    }
}