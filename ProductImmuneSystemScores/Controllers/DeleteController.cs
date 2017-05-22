using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductImmuneSystemScores.ApiClient;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.Controllers
{
    public class DeleteController : Controller
    {
        private readonly ProductScoreApiWrapper _productScoreApiWrapper;

        public DeleteController(IProductScoresApiConfig productScoresApiConfig)
        {
            //TODO: I'm still not happy with creating a new HTTPCLient all the time
            _productScoreApiWrapper = new ProductScoreApiWrapper(new HttpClient(), productScoresApiConfig);
        }

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
            var response = await _productScoreApiWrapper.DeleteProduct(id);

            if (!response.IsSuccessStatusCode)
                return View("~/Views/Delete/Error.cshtml");

            return RedirectToAction("index", "home");
        }
    }
}