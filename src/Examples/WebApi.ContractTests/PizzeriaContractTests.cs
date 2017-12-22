using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NContract;
using NContract.NUnit;
using NUnit.Framework;

namespace WebApi.ContractTests
{
    [TestFixture]
    public class PizzeriaContractTests : RunContractTests
    {
        private const string BaseUri = "http://localhost:59210";

        [Test]
        public async Task WhenIGetAllOrders_IShouldReceiveAListOfOrders()
        {
            var configureGet = new RestApiClientConfigurationBuilder()
                .WithBaseUri(BaseUri)
                .WithRequestUri("/pizzeria/orders")
                .WithContentType("application/json")
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            var invocationResult = await InvokeApiAsync(configureGet);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
            Assert.IsNotNull(invocationResult.StringContent);
        }

        [Test]
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

            Assert.AreEqual(HttpStatusCode.Created, invocationResult.HttpResponseMessage.StatusCode);
            Assert.DoesNotThrow(() => Guid.Parse(invocationResult.StringContent.orderId.Value));
        }

        [Test]
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

            Assert.AreEqual(HttpStatusCode.Created, invocationResult.HttpResponseMessage.StatusCode);
            var orderId = invocationResult.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

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

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);

            //Get order
            var configureGet = new RestApiClientConfigurationBuilder()
                  .WithBaseUri(BaseUri)
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            invocationResult = await InvokeApiAsync(configureGet);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
            Assert.AreEqual(3, invocationResult.StringContent.pizzas.Count);
        }

        [Test]
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
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //delete it
            var configureDelete = new RestApiClientConfigurationBuilder()
                 .WithBaseUri(BaseUri)
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            invocationResult = await InvokeApiAsync(configureDelete);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.HttpResponseMessage.StatusCode);
        }
    }
}
