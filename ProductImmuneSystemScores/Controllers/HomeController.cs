using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductImmuneSystemScores.ApiClient;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductScoresApiConfig _configuration;

        public HomeController(IProductScoresApiConfig configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActionResult> Index()
        {
            var apiClient = new ProductScoreApiWrapper(new HttpClient(),  _configuration);
            var response = await apiClient.GetProducts();

            if (!response.IsSuccessStatusCode)
                return View(new ProductScoresViewModel());

            var productScores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductScore>>(await response.Content.ReadAsStringAsync());

            var result = new ProductScoresViewModel {Scores = productScores};

            return View(result);
        }

        public ActionResult Test()
        {
            throw new System.Exception();
        }
        public ActionResult AnotherTest()
        {
            return HttpNotFound();
        }
        public ActionResult test400()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}