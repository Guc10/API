using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
    }
}
