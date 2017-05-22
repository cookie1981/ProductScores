using api.DatabaseProviders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class User : IHaveAnId<string>
    {
        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_Id")]
        public string Id { get; set; }
    }
}