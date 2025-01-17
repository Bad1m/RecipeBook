﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReviewMicroservice.Domain.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("RecipeId")]
        public int RecipeId { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Comment")]
        public string Comment { get; set; }
        [BsonElement("Rating")]
        public double Rating { get; set; }
        [BsonElement("Date")]
        public DateTime Date { get; set; }
        [BsonIgnore]
        public Recipe Recipe { get; set; }
        [BsonIgnore]
        public User User { get; set; }
    }
}