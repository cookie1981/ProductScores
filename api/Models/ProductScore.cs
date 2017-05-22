using System.ComponentModel.DataAnnotations;
using api.DatabaseProviders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class ProductScore : IHaveAnId<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Product is required", AllowEmptyStrings = false)]
        [BsonRequired]
        [BsonElement("product")]
        public string Product { get; set; }
        [BsonElement("team")]
        public string Team { get; set; }
        [BsonElement("SDET")]
        public string Sdet { get; set; }

        [BsonElement("immuneSystem")]
        public ImmuneSystem ImmuneSystem { get; set; }
    }
}