using System.Linq;

namespace ProductImmuneSystemScores.Models
{
    public static class ExtensionMethods
    {
        public static int CategoryScore(this ProductScore productScore, string category)
        {
            try
            {
                return productScore.ImmuneSystem.Scores.Single(x => x.Category.ToLower() == category).Grade;
            }
            catch
            {
                return Constants.DefaultImmuneSystemScore;
            }
        }
    }
}