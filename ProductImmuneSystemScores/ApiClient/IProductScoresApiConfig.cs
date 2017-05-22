namespace ProductImmuneSystemScores.ApiClient
{
    public interface IProductScoresApiConfig
    {
        UrlElement Api { get; set; }
        AuthenticationElement Authentication { get; set; }
    }
}