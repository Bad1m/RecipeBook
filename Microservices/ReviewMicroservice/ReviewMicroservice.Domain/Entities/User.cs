using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReviewMicroservice.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
    }
}