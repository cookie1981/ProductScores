using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProductImmuneSystemScores.ApiClient;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.Controllers
{
    public class EditController : Controller
    {
        private ProductScoreApiWrapper _productScoreApiWrapper;

        public EditController(IProductScoresApiConfig productScoreApiConfiguration)
        {
            //TODO: I'm still not happy with creating a new HTTPCLient all the time
            _productScoreApiWrapper = new ProductScoreApiWrapper(new HttpClient(), productScoreApiConfiguration);
        }

        private const string DefaultProductscoreid = "0";

        [HttpGet]
        [Route("/Index/Create/")]
        public ActionResult Create()
        {
            return View("~/Views/Edit/Index.cshtml", DefaultProductScoreViewModel());
        }

        [HttpGet]
        public async Task<ActionResult> Index(string id)
        {
            var viewModel = DefaultProductScoreViewModel();
            viewModel.DisplayValidationErrors = false;

            if (string.IsNullOrEmpty(id) && id != DefaultProductscoreid)
            {
                //TODO: view Should display an error message!
                return View(viewModel);
            }

            var response = await LookupSelectedProduct(id);

            if (!response.IsSuccessStatusCode)
            {
                //TODO: Error msg - product not found
                return View(viewModel);
            }

            viewModel = MapProductScoreToViewModel(await response.Content.ReadAsStringAsync(), viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProductScoreViewModel productScoreToSave)
        {
            if (ModelState.IsValid)
            {
                var creatingNewProduct = CreatingANewProduct(productScoreToSave);

                var product = MapProductScoreViewModelToProductScore(productScoreToSave, creatingNewProduct);
                HttpResponseMessage result = null; 
                    
                if (creatingNewProduct)
                    result = await _productScoreApiWrapper.CreateNew(product).ConfigureAwait(false);
                else
                    result = await _productScoreApiWrapper.UpdateProduct(product).ConfigureAwait(false);

                productScoreToSave.SaveSucceeded = result.IsSuccessStatusCode;
            }
            else
            {
                productScoreToSave.SaveSucceeded = false;
            }
            productScoreToSave.DisplayValidationErrors = true;
            productScoreToSave.TeamOptions = BuildTeamList();
            var options = BuildImmuneSystemScoreOptionsList();

            productScoreToSave.AutomationScoreOptions = options;
            productScoreToSave.CrossBrowserScoreOptions = options;
            productScoreToSave.MonitoringScoreOptions = options;
            productScoreToSave.ReleaseScoreOptions = options;
            productScoreToSave.TechDebtScoreOptions = options;
            productScoreToSave.SecurityScoreOptions = options;
            productScoreToSave.PerformanceScoreOptions = options;

            return View(productScoreToSave);
        }

        private static ProductScore MapProductScoreViewModelToProductScore(ProductScoreViewModel newProductScore, bool isNewProduct)
        {
            var scoreToSave = new ProductScore();
            
            if (!isNewProduct)
                scoreToSave.Id = newProductScore.Id;

            scoreToSave.Product = newProductScore.Product;
            scoreToSave.Sdet = newProductScore.SDET;
            scoreToSave.Team = newProductScore.SelectedTeam;
            scoreToSave.ImmuneSystem = new ImmuneSystem();
            scoreToSave.ImmuneSystem.Scores = new List<Score>
            {
                new Score() {Category = Constants.AUTOMATION, Grade = newProductScore.SelectedAutomationScore},
                new Score() {Category = Constants.CROSSBROWSER, Grade = newProductScore.SelectedCrossBrowserScore},
                new Score() {Category = Constants.MONITORING_AND_ALERTING, Grade = newProductScore.SelectedMonitoringScore},
                new Score() {Category = Constants.SECURITY, Grade = newProductScore.SelectedSecurityScore},
                new Score() {Category = Constants.PERFORMANCE, Grade = newProductScore.SelectedPerformanceScore},
                new Score() {Category = Constants.TECH_DEBT, Grade = newProductScore.SelectedTechDebtScore},
                new Score() {Category = Constants.RELEASE, Grade = newProductScore.SelectedReleaseScore}
            };

            return scoreToSave;
        }

        private static bool CreatingANewProduct(ProductScoreViewModel newProductScore)
        {
            return newProductScore.Id == DefaultProductscoreid;
        }

        private static ProductScoreViewModel MapProductScoreToViewModel(string jsonResponseBody, ProductScoreViewModel viewModel)
        {
            //TODO: This could fail
            var productScore = JsonConvert.DeserializeObject<ProductScore>(jsonResponseBody);

            viewModel.Id = productScore.Id;
            viewModel.Product = productScore.Product;
            viewModel.SDET = productScore.Sdet;

            viewModel.SelectedTeam = productScore.Team;
            viewModel.SelectedAutomationScore = productScore.CategoryScore(Constants.AUTOMATION);
            viewModel.SelectedCrossBrowserScore = productScore.CategoryScore(Constants.CROSSBROWSER);
            viewModel.SelectedMonitoringScore = productScore.CategoryScore(Constants.MONITORING_AND_ALERTING);
            viewModel.SelectedReleaseScore = productScore.CategoryScore(Constants.RELEASE);
            viewModel.SelectedTechDebtScore = productScore.CategoryScore(Constants.TECH_DEBT);
            viewModel.SelectedSecurityScore = productScore.CategoryScore(Constants.SECURITY);
            viewModel.SelectedPerformanceScore = productScore.CategoryScore(Constants.PERFORMANCE);

            return viewModel;
        }

        private async Task<HttpResponseMessage> LookupSelectedProduct(string id)
        {
            return await _productScoreApiWrapper.GetProduct(id);
        }

        private static List<SelectListItem> BuildImmuneSystemScoreOptionsList()
        {
            var options = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "0"},
                new SelectListItem {Value = "1", Text = "1"},
                new SelectListItem {Value = "2", Text = "2"},
                new SelectListItem {Value = "3", Text = "3"},
                new SelectListItem {Value = "4", Text = "4"},
                new SelectListItem {Value = "5", Text = "5"}
            };
            return options;
        }

        private static List<SelectListItem> BuildTeamList()
        {
            //TODO: Get List From Database?
            var teams = new List<SelectListItem>
            {
                new SelectListItem {Value = string.Empty, Text = string.Empty},
                new SelectListItem {Value = "Motor", Text = "Motor"},
                new SelectListItem {Value = "MyCtm", Text = "MyCtm"},
                new SelectListItem {Value = "Home", Text = "Home"},
                new SelectListItem {Value = "MicroMachines", Text = "MicroMachines"},
                new SelectListItem {Value = "Kinetics", Text = "Kinetics"},
                new SelectListItem {Value = "The Mob", Text = "The Mob"},
                new SelectListItem {Value = "MIT", Text = "MIT"},
                new SelectListItem {Value = "Transformers", Text = "Transformers"},
                new SelectListItem {Value = "Quoting", Text = "Quoting"},
                new SelectListItem {Value = "All Spark", Text = "All Spark"},
                new SelectListItem {Value = "Platform", Text = "Platform"},
                new SelectListItem {Value = "42", Text = "42"},
            };
            return teams;
        }

        private static ProductScoreViewModel DefaultProductScoreViewModel()
        {
            var options = BuildImmuneSystemScoreOptionsList();
            var teams = BuildTeamList();

            var defaultProductScoreViewModel = new ProductScoreViewModel
            {
                Id = DefaultProductscoreid,
                TeamOptions = teams,
                AutomationScoreOptions = options,
                CrossBrowserScoreOptions = options,
                MonitoringScoreOptions = options, 
                ReleaseScoreOptions = options, 
                TechDebtScoreOptions = options,
                SecurityScoreOptions = options,
                PerformanceScoreOptions = options,  
            };

            return defaultProductScoreViewModel;
        }
    }
}