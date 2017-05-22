using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class Score
    {
        [BsonElement("category")]
        public string Category { get; set; }
        [BsonElement("grade")]
        public int Grade { get; set; }
//        [BsonElement("justifications")]
//        public List<string> Justifications { get; set; }
    
    }
}