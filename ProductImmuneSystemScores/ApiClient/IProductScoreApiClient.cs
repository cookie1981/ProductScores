using System.Net.Http;
using System.Threading.Tasks;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.ApiClient
{
    public interface IProductScoreApiClient
    {
        Task<HttpResponseMessage> DeleteProduct(string productId);
        Task<HttpResponseMessage> GetProduct(string productId);
        Task<HttpResponseMessage> GetProducts();
        Task<HttpResponseMessage> UpdateProduct(ProductScore productToUpdate);
    }
}