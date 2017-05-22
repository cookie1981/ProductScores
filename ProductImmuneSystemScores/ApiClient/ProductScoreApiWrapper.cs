using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductImmuneSystemScores.Models;

namespace ProductImmuneSystemScores.ApiClient
{
    public class ProductScoreApiWrapper : IProductScoreApiClient
    {
        private readonly HttpClient _httpClient;
        readonly Uri _productscoresApiUri;
        
        public ProductScoreApiWrapper(HttpClient productScoresApi, IProductScoresApiConfig config)
        {
            _httpClient = productScoresApi;
            _productscoresApiUri = new Uri(config.Api.Url);
            var token = GetAuthenticationToken(config.Authentication.Url, config.Authentication.Username, config.Authentication.Password).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        private async Task<Token> GetAuthenticationToken(string authenticationServiceUrl, string username, string password)
        {
            Token token = null;

            try
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };
                var content = new FormUrlEncodedContent(pairs);

                _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                var response = await _httpClient.PostAsync(authenticationServiceUrl, content).ConfigureAwait(false);
                token = (Token)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(Token));
            }
            catch (Exception)
            {
                //TODO: handle this!
            }
            finally {
                _httpClient.DefaultRequestHeaders.Remove("X-Requested-With");
            }

            return token;
        }

        public async Task<HttpResponseMessage> DeleteProduct(string productId)
        {
            var requestUri = new Uri(_productscoresApiUri, productId);
            
            var response = await _httpClient.DeleteAsync(requestUri);

            return response;
        }

        public async Task<HttpResponseMessage> GetProduct(string productId)
        {
            var requestUri = new Uri(_productscoresApiUri, productId);
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetProducts()
        {
            return await _httpClient.GetAsync(_productscoresApiUri);
        }

        public async Task<HttpResponseMessage> UpdateProduct(ProductScore productScore)
        {
            var byteContent = ConvertProductToByteArrayContent(productScore);

            return await _httpClient.PutAsync(_productscoresApiUri.AbsoluteUri, byteContent).ConfigureAwait(false);
        }

        private class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
            [JsonProperty("error")]
            public string Error { get; set; }
        }

        public async Task<HttpResponseMessage> CreateNew(ProductScore newProductScore)
        {
            var byteContent = ConvertProductToByteArrayContent(newProductScore);

            return await _httpClient.PostAsync(_productscoresApiUri.AbsoluteUri, byteContent).ConfigureAwait(false);
        }

        private static ByteArrayContent ConvertProductToByteArrayContent(ProductScore productScore)
        {
            var json = JsonConvert.SerializeObject(productScore);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}