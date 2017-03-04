using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NContract.Core;
using NContract.Core.RestApi;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace WebApi.Tests.Contract
{
    [TestFixture]
    public class ApiContractTests2 : ContractTestBase
    {
        private readonly string _baseUri;

        public ApiContractTests2()
        {
            _baseUri = ConfigurationManager.AppSettings["TestEndpoint"];
        }

        [SetUp]
        public void MySetup()
        {
        }

        [Test]
        public async void WhenInvokeGet_Ok200StatusIsReturned2()
        {
            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithRequestUri("/version")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            if (RestApiClientFactory != null)
            {
                var client = RestApiClientFactory.Create(configuration);

                var result = await client.Invoke();

                Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);
            }
        }

        [Test]
        public void WhenInvokePost_AndHeadersAreValid_Ok200StatusIsReturned()
        {
            dynamic model = new JObject();
            model.name = "Matteo";

            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithRequestUri("/header")
                .WithModel(model)
                .WithHeaders(new RequestHeadersContainer()
                {
                    Accept = new List<MediaTypeWithQualityHeaderValue>() { new MediaTypeWithQualityHeaderValue("application/json") }
                })
                .WithResponseContentType(ResponseContentType.NoContent)
                .WithHttpMethod(HttpMethod.Post)
                .Build();

            var result = RestApiClientFactory.Create(configuration)
                .Invoke();

            Assert.AreEqual(HttpStatusCode.OK, result.Result.HttpResponseMessage.StatusCode);
        }

        [Test]
        public void WhenInvokePost_AndRequestIsValid_200OKStatusIsReturned2()
        {
            dynamic request = new JObject();
            request.name = "Matteo";

            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithModel(request)
                .WithRequestUri("/create")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Post)
                .Build();

            var result = RestApiClientFactory.Create(configuration)
                .Invoke().Result;

            Assert.AreEqual(HttpStatusCode.OK,  result.HttpResponseMessage.StatusCode);
            Assert.AreEqual(request.name.Value, result.StringContent.name.Value);
            Assert.DoesNotThrow(() => Guid.Parse(result.StringContent.id.Value));
        }

        [Test]
        public void WhenInvokePost_AndRequestIsNotValid_Then403BadRequestStatusIsReturned2()
        {
            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithEmptyModel()
                .WithRequestUri("/create")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Post)
                .Build();

            var result = RestApiClientFactory.Create(configuration)
                .Invoke();

            Assert.AreEqual(HttpStatusCode.BadRequest, result.Result.HttpResponseMessage.StatusCode);
            //Assert.AreEqual("Invalid request", result.Result.StringContent.message.ToString());
        }

        [Test]
        public void WhenInvokePut_AndRequestIsValid_200OKStatusIsReturned2()
        {
            dynamic request = new JObject();
            request.id = Guid.NewGuid();

            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithModel(request)
                .WithRequestUri("/put")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Put)
                .Build();

            var result = RestApiClientFactory.Create(configuration)
                .Invoke();

            Assert.AreEqual(HttpStatusCode.OK, result.Result.HttpResponseMessage.StatusCode);
            Assert.AreEqual(request.id.ToString(), result.Result.StringContent.id.Value);
            Assert.AreEqual("entity updated", result.Result.StringContent.name.Value);
        }

        [Test]
        public void WhenInvokePut_AndRequestIsNotValid_403BadRequestStatusIsReturned2()
        {
            dynamic request = new JObject();
            request.id = Guid.Empty;

            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithModel(request)
                .WithRequestUri("/put")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Put)
                .Build();

            var result = RestApiClientFactory.Create(configuration)
                .Invoke();

            Assert.AreEqual(HttpStatusCode.BadRequest, result.Result.HttpResponseMessage.StatusCode);
            Assert.AreEqual("Invalid request", result.Result.StringContent.message.Value);
        }
    }
}
