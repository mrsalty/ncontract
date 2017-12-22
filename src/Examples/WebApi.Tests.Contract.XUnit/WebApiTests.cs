using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NContract;
using NContract.XUnit;
using Xunit;

namespace WebApi.Tests.Contract.XUnit
{
    public class WebApiTests : RunContractTests
    {
        private const string BaseUri = "http://localhost:59210";

        [Fact]
        [ContractTest]
        public async Task XUnit_WhenIGetAllOrders_IShouldReceiveAListOfOrders()
        {
            var configureGet = new RestApiClientConfigurationBuilder()
                .WithBaseUri(BaseUri)
                .WithRequestUri("/pizzeria/orders")
                .WithContentType("application/json")
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            var invocationResult = await InvokeApiAsync(configureGet);

            Assert.Equal(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
            Assert.NotNull(invocationResult.StringContent);
        }

        [Fact]
        public async Task WhenIOrder2Pizzas_ThenIShouldGetMyOrderId()
        {
            var order = new
            {
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = await InvokeApiAsync(configurePost);

            Assert.Equal(HttpStatusCode.Created, invocationResult.HttpResponseMessage.StatusCode);
            Guid.Parse(invocationResult.StringContent.orderId.Value);
        }

        [Fact]
        public async Task WhenIUpdateMyOrderTo3Pizzas_ThenIShouldGet3Pizzas()
        {
            //post new order
            var order = new
            {
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = await InvokeApiAsync(configurePost);

            Assert.Equal(HttpStatusCode.Created, invocationResult.HttpResponseMessage.StatusCode);
            var orderId = invocationResult.StringContent.orderId.Value;
           Guid.Parse(orderId);

            //update order
            var updated = new[]
            {
                new {pizzaType = "Diavola"},
                new {pizzaType = "Margherita"},
                new {pizzaType = "QuattroStagioni"}
            };

            var configurePut = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithHttpMethod(HttpMethod.Put)
                  .Build();

            invocationResult = await InvokeApiAsync(configurePut);

            Assert.Equal(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);

            //Get order
            var configureGet = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            invocationResult = await InvokeApiAsync(configureGet);

            Assert.Equal(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
            Assert.Equal(3, invocationResult.StringContent.pizzas.Count);
        }

        [Fact]
        public async Task WhenICancelMyOrder_ThenIShouldntGetAnyPizza()
        {
            //create new order
            var order = new
            {
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = await InvokeApiAsync(configurePost);
            var orderId = invocationResult.StringContent.orderId.Value;
            Guid.Parse(orderId);

            //delete it
            var configureDelete = new RestApiClientConfigurationBuilder()
                 .WithBaseUri(BaseUri)
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            invocationResult = await InvokeApiAsync(configureDelete);

            Assert.Equal(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
        }
    }
}
