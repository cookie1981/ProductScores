namespace ProductImmuneSystemScores.Models
{
    public class ProductScore 
    {
        public string Id { get; set; }
        public string Product { get; set; }
        public string Team { get; set; }
        public string Sdet { get; set; }
        public ImmuneSystem ImmuneSystem { get; set; }
    }
}