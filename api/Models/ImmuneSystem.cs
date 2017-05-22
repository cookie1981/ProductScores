using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class ImmuneSystem
    {
        [BsonElement("scores")]
        public List<Score> Scores { get; set; }
    }
}