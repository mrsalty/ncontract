using System;
using System.Net;
using System.Net.Http;
using NContract;
using NContract.FluentRestApi;
using NUnit.Framework;

namespace WebApi.Tests.Contract
{
    [TestFixture]
    public class PizzeriaContractTests : ContractTestBase
    {
        [Test]
        public void WhenIGetAllOrders_IShouldReceiveAListOfOrders()
        {
            var configureGet = new RestApiClientConfigurationBuilder()
                .WithBaseUri("http://localhost:52730")
                .WithRequestUri("/pizzeria/orders")
                .WithContentType("application/json")
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            var invocationResult = InvokeApi(configureGet);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);
            Assert.IsNotNull(invocationResult.Result.StringContent);
        }

        [Test]
        public void WhenIOrder2Pizzas_ThenIShouldGetMyOrderId()
        {
            var order = new
            {
                tableId = Guid.NewGuid().ToString(),
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = InvokeApi(configurePost);

            Assert.AreEqual(HttpStatusCode.Created, invocationResult.Result.HttpResponseMessage.StatusCode);
            Assert.DoesNotThrow(() => Guid.Parse(invocationResult.Result.StringContent.orderId.Value));
        }

        [Test]
        public void WhenIUpdateMyOrderTo3Pizzas_ThenIShouldGet3Pizzas()
        {
            //post new order
            var order = new
            {
                tableId = Guid.NewGuid().ToString(),
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = InvokeApi(configurePost);

            Assert.AreEqual(HttpStatusCode.Created, invocationResult.Result.HttpResponseMessage.StatusCode);
            var orderId = invocationResult.Result.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //update order
            var updated = new[]
            {
                new {pizzaType = "Diavola"},
                new {pizzaType = "Margherita"},
                new {pizzaType = "QuattroStagioni"}
            };

            var configurePut = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithHttpMethod(HttpMethod.Put)
                  .Build();

            invocationResult = InvokeApi(configurePut);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);

            //Get order
            var configureGet = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            invocationResult = InvokeApi(configureGet);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);
            Assert.AreEqual(3, invocationResult.Result.StringContent.pizzas.Count);
        }

        [Test]
        public void WhenICancelMyOrder_ThenIShouldntGetAnyPizza()
        {
            //create new order
            var order = new
            {
                tableId = Guid.NewGuid().ToString(),
                pizzas = new[]
                {
                     new { pizzaType = "Margherita" },
                     new { pizzaType = "Napoli" }
                }
            };

            var configurePost = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = InvokeApi(configurePost);
            var orderId = invocationResult.Result.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //delete it
            var configureDelete = new RestApiClientConfigurationBuilder()
                 .WithBaseUri("http://localhost:52730")
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            invocationResult = InvokeApi(configureDelete);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);
        }
    }
}
