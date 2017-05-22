using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductImmuneSystemScores.ApiClient;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.Controllers
{
    public class DeleteController : Controller
    {
        public ActionResult Index(string id, string productName)
        {
            //TODO Validate incoming params
            return View(new DeleteProductScoreViewModel()
            {
                Id = id,
                ProductName = productName
            });
        }

        public async Task<ActionResult> Confirm(string id)
        {
            ProductScoreApiSection config = (ProductScoreApiSection)System.Configuration.ConfigurationManager.GetSection("ProductScoreApiGroup/ProductScoreApiConfiguration");

            //TODO: Sort this out
            var response = await new ProductScoreApiWrapper(new HttpClient(), config).DeleteProduct(id);

            if (!response.IsSuccessStatusCode)
                return View("~/Views/Delete/Error.cshtml");

            return RedirectToAction("index", "home");
        }
    }
}