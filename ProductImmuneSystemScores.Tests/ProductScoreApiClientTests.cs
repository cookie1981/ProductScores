using System;
using System.Collections.Generic;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using ProductImmuneSystemScores.ApiClient;

namespace ProductImmuneSystemScores.Tests
{
    [TestFixture]
    public class ProductScoreApiClientTests
    {
        [Test]
        public void ShouldUseConfigurationCorrectly()
        {
            IProductScoresApiConfig productScoresApiConfig = new ProductScoreApiSection();
            productScoresApiConfig.Api.Url = "http://itdoesntmatter.com/";
            productScoresApiConfig.Authentication.Url = "http://itdoesntmatter.com/Token";
            productScoresApiConfig.Authentication.Username = "anyone";
            productScoresApiConfig.Authentication.Password = "password";
            
            var httpClient = new MockHttpMessageHandler();
            const string expectedToken = @"uJrWCd-ejX5pTfyKgQOCqA14V6iBuIuWTa3LSNcu9ebmbl4rcwhWCPDAvLGlbyt2PmBYOMiNurTzs6iZmpiR8S9uo54__rQqhLT1MzRtBoMj1bD9NiN9G7aY-fZC0RUgMHyk3vnEpEqO3h-LSSh1VBl8aH1Y2a92uUk2y6vi-gQbfHXJpI0ftFUZ_UvrZHaZIqadDRwc12Ls4L6Na1SJMQ";

            const string expectedJsonResponse = "{\r\n    \"access_token\": \"" + expectedToken + "\",\r\n    \"token_type\": \"bearer\",\r\n    \"expires_in\": 1209599\r\n}";
            httpClient.Expect(productScoresApiConfig.Authentication.Url)
                      .WithFormData("username=anyone&password=password&grant_type=password")
                      .Respond("application/json", expectedJsonResponse);

            httpClient.Expect(productScoresApiConfig.Api.Url)
                        //.WithHeaders("Bearer" , expectedToken)
                        .Respond("application/json", "{}");
            
            var productScoreApiWrapper = new ProductScoreApiWrapper(httpClient.ToHttpClient(), productScoresApiConfig);

            var results = productScoreApiWrapper.GetProducts();

            httpClient.VerifyNoOutstandingExpectation();
            Assert.True(results.Result.IsSuccessStatusCode);
            Assert.That(results.Result.Content.ReadAsStringAsync().Result, Is.EqualTo("{}"));
        }
    }
}