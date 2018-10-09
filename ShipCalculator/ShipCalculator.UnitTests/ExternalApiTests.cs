using Calculator_BLL.API;
using Calculator_BLL.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShipCalculator.UnitTests
{
    [TestClass]
    public class ExternalApiTests
    {
        [TestMethod]
        public void GetAllShips_returnsList()
        {

            var api = new ExternalApi(new HttpClient());
            var results = api.GetAllShips();

            Assert.IsNotNull(results);
            Assert.IsTrue(results[0].MGLT != string.Empty);
        }

        [TestMethod]
        public void GetAllShips_2Async()
        {
            try
            {

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                handlerMock
                   .Protected()
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent("{'count': 37,'next': null,'previous': null, 'results': [{ " +
                       "'name': 'Executor','model': 'Executor-class star dreadnought','manufacturer': 'Kuat Drive Yards, Fondor Shipyards','cost_in_credits': '1143350000', " +
                       "'length': '19000','max_atmosphering_speed': 'n/a','crew': '279144','passengers': '38000','cargo_capacity': '250000000','consumables': '6 years','hyperdrive_rating': '2.0'," +
                       "'MGLT': '40','starship_class': 'Star dreadnought','pilots': [],'films': ['https://swapi.co/api/films/2/','https://swapi.co/api/films/3/'],'created': '2014-12-15T12:31:42.547000Z'," +
                       "'edited': '2017-04-19T10:56:06.685592Z','url': 'https://swapi.co/api/starships/15/'}]}", Encoding.UTF8, "application/json"),
                   })
                   .Verifiable();

                // use real http client with mocked handler here
                var httpClient = new HttpClient(handlerMock.Object)
                {
                    BaseAddress = new Uri(StringHelper.urlbase),
                };

                var subjectUnderTest = new ExternalApi(httpClient);

                var result = subjectUnderTest
                   .GetAllShips();

                // ASSERT
                Assert.IsNotNull(result);
                Assert.AreEqual(result[0].name, "Executor");
                Assert.AreEqual(result[0].MGLT, "40");
                Assert.AreEqual(result[0].model, "Executor-class star dreadnought");
                Assert.AreEqual(result[0].manufacturer, "Kuat Drive Yards, Fondor Shipyards");
                Assert.AreEqual(result[0].cargo_capacity, "250000000");
                Assert.AreEqual(result[0].consumables, "6 years");


                // also check the 'http' call was like we expected it
                var expectedUri = new Uri(StringHelper.urlbase + StringHelper.getStarships);

                handlerMock.Protected().Verify(
                   "SendAsync",
                   Times.Exactly(1), // we expected a single external request
                   ItExpr.Is<HttpRequestMessage>(req =>
                      req.Method == HttpMethod.Get  // we expected a GET request
                      && req.RequestUri == expectedUri // to this uri
                   ),
                   ItExpr.IsAny<CancellationToken>()
                );

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
