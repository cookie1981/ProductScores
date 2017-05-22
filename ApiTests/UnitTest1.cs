using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using api.Controllers;
using api.DatabaseProviders;
using api.Models;
using Moq;
using NUnit.Framework;

namespace ProductScores.Api.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            //Arrange
            var fakeResults = new List<ProductScore>
            {
                new ProductScore()
                {
                    Id = "x",
                    Product = "Car",
                    Sdet = "",
                    Team = "motor",
                    ImmuneSystem = new ImmuneSystem()
                    {
                        Scores = new List<Score>()
                        {
                            new Score()
                            {
                                Category = "x-browser",
                                Grade = 3
                            }
                        }
                    }
                }
            };

            var mockProductScoreRepository = new Mock<IDatabaseRepository<ProductScore>>();
            mockProductScoreRepository.Setup(x => x.FindAllAsync()).Returns(Task.FromResult(fakeResults));
            
            var controller = new ProductScoreController(mockProductScoreRepository.Object);

            //Act
            var actionResult = controller.GetProductScoresAsync();
//            var contentResult = actionResult.Result as OkNegotiatedContentResult<List<ProductScore>>;
//
//            //Assert
//            Assert.IsNotNull(contentResult);
//            Assert.IsNotNull(contentResult.Content);
//            Assert.That(contentResult.Content[0].Team , Is.EqualTo("motor"));
//            Assert.That(contentResult.Content[0].Sdet, Is.EqualTo(""));
        }
    }
}
