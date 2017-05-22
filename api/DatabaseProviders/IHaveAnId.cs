namespace api.DatabaseProviders
{
    public interface IHaveAnId<T>
    {
        string Id { get; set; }
    }
}