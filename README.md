# NContract
test against your rest webapi contracts quickly!

```C#
	[TestFixture]
    public class PizzeriaContractTests : ContractTestBase
    {
        [Test]
        public void WhenIGetAllOrders_IShouldReceiveAListOfOrders()
        {
            var configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            var invocationResult = ApiInvoke(configuration);

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

            var configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = ApiInvoke(configuration);

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

            var configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = ApiInvoke(configuration);

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

            configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Put)
                  .Build();

            invocationResult = ApiInvoke(configuration);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);

            //Get order
            configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            invocationResult = ApiInvoke(configuration);

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

            var configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri("/pizzeria/orders")
                  .WithContentType("application/json")
                  .WithModel(order)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Post)
                  .Build();

            var invocationResult = ApiInvoke(configuration);
            var orderId = invocationResult.Result.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //delete it
            var configuration2 = new RestApiClientConfigurationBuilder()
                 .WithBaseUri("http://localhost:52730")
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithResponseContentType(ResponseContentType.String)
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            invocationResult = ApiInvoke(configuration2);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);
        }
    }
```

It will generate this html report : 

<img src="/report1.png" alt="NContract html report">
