using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using api.Models;
using System.Net.Http;
using api.DatabaseProviders;
using MongoDB.Bson;

namespace api.Controllers
{
    [Authorize] 
    public class ProductScoreController : ApiController
    {
        private const string PRODUCTSCORESROUTE = "api/ProductScores";

        private IDatabaseRepository<ProductScore> ProductStoreRepository { get; }
//        public ILogger Logger { get; }

        public ProductScoreController(IDatabaseRepository<ProductScore> productStoreRepository)
        {
            ProductStoreRepository = productStoreRepository;
//            Logger = logger;
        }

        [AllowAnonymous]
        [HttpOptions]
        [Route(PRODUCTSCORESROUTE)]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        [HttpGet]
        [Route(PRODUCTSCORESROUTE)]
        public async Task<IHttpActionResult> GetProductScoresAsync()
        {
            try
            {
                var productScores = await ProductStoreRepository.FindAllAsync().ConfigureAwait(false);

                if (productScores.Count > 0)
                    return Ok(productScores);

                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            catch (Exception e)
            {
                //Logger
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route(PRODUCTSCORESROUTE + "/{productId}", Name= "GetProductScoreById")]
        public async Task<IHttpActionResult> GetProductScoreByIdAsync(string productId)
        {
            try
            {
                if (!ValidateProductId(productId))
                    return BadRequest("Specified value is not a valid productId");

                var productScores = await ProductStoreRepository.FindByIdAsync(productId).ConfigureAwait(false);

                if (string.IsNullOrEmpty(productScores.Id.ToString()))
                    return NotFound();

                return Ok(productScores);
            }
            catch (Exception e)
            {
                //log the exception detail
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route(PRODUCTSCORESROUTE)]
        public async Task<IHttpActionResult> PostAsync(ProductScore productScore)
        {
            //            if (ModelState.IsValid)

            //TODO: Should I be using a JsonObject here instead of a typed object?
            //if so why? It works with a typed object
            //There will be an overhead of deserialising the JSON into an object, but how much of a performance hit does that cause?

            if (!ValidProductScore(productScore/*, false*/))
                return BadRequest("Supplied productScore is Invalid.");

            try
            {
                productScore.LastUpdated = DateTime.UtcNow;
                var insertOccured = await ProductStoreRepository.InsertAsync(productScore).ConfigureAwait(false);

                if (!insertOccured || string.IsNullOrEmpty(productScore.Id.ToString() ))
                    // but why was it not saved, and no exception was thrown?!?!
                    return InternalServerError();

                var location = new Uri(Url.Link("GetProductScoreById", new {productId = productScore.Id}));

                return Created(location, productScore);
            }
            catch (Exception e)
            {
                //log the exception detail
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route(PRODUCTSCORESROUTE)]
        public async Task<IHttpActionResult> PutAsync(ProductScore productScore)
        {
            try
            {
                if (!ValidProductScore(productScore))
                    return BadRequest("Supplied productScore is Invalid.");

                productScore.LastUpdated = DateTime.UtcNow;
                var updateOccured = await ProductStoreRepository.UpdateAsync(productScore).ConfigureAwait(false);

                if (updateOccured)
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                //log the exception
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route(PRODUCTSCORESROUTE + "/{productId}")]
        public async Task<IHttpActionResult> DeleteAsync(string productId)
        {
            try
            {
                if (!ValidateProductId(productId))
                    return BadRequest("Specified value is not a valid productId");
                
                var deleteOccured = await ProductStoreRepository.DeleteAsync(productId).ConfigureAwait(false);

                if (deleteOccured)
                    return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));

                return NotFound();
            }
            catch (Exception e)
            {
                //log exception
                return InternalServerError();
            }
        }

        private bool ValidProductScore(ProductScore productScore)
        {
            //            return ValidProductScore(productScore, true);
            //        }
            //
            //        private bool ValidProductScore(ProductScore productScore, bool validateProductId)
            //        {
            //            var validProductId = (validateProductId ? ValidateProductId(productScore.Id) : true);
            //TODO: output a list of validation errors

            return (/*validProductId && */ !string.IsNullOrEmpty(productScore.Product) && productScore.ImmuneSystem != null);
        }

        private bool ValidateProductId(string productId)
        {
            ObjectId objectId;

            return ObjectId.TryParse(productId, out objectId);
        }
    }
}
