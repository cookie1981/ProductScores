using System;

namespace ProductImmuneSystemScores.Models
{
    public class ProductScore 
    {
        private readonly DateTime _defaultDateTime = DateTime.MinValue;

        public ProductScore()
        {
            LastUpdated = _defaultDateTime;
        }

        public string Id { get; set; }
        public string Product { get; set; }
        public string Team { get; set; }
        public string Sdet { get; set; }
        public ImmuneSystem ImmuneSystem { get; set; }
        public DateTime LastUpdated { get; set; }

        public string LastUpdatedOrDefault => LastUpdated == _defaultDateTime ? "Unknown" : LastUpdated.ToShortDateString();
    }
}