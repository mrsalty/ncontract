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

            var result = RestApiClientFactory.Create(configuration).Invoke();

            Assert.AreEqual(HttpStatusCode.OK, result.Result.HttpResponseMessage.StatusCode);
            Assert.IsNotNull(result.Result.StringContent);
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

            var result = RestApiClientFactory.Create(configuration).Invoke();

            Assert.AreEqual(HttpStatusCode.Created, result.Result.HttpResponseMessage.StatusCode);
            Assert.DoesNotThrow(() => Guid.Parse(result.Result.StringContent.orderId.Value));
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

            var result = RestApiClientFactory.Create(configuration).Invoke().Result;

            Assert.AreEqual(HttpStatusCode.Created, result.HttpResponseMessage.StatusCode);
            var orderId = result.StringContent.orderId.Value;
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

            result = RestApiClientFactory.Create(configuration).Invoke().Result;

            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //Get order
            configuration = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithResponseContentType(ResponseContentType.String)
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            result = RestApiClientFactory.Create(configuration).Invoke().Result;

            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);
            Assert.AreEqual(3, result.StringContent.pizzas.Count);
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

            var result = RestApiClientFactory.Create(configuration).Invoke().Result;
            var orderId = result.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //delete it
            var configuration2 = new RestApiClientConfigurationBuilder()
                 .WithBaseUri("http://localhost:52730")
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithResponseContentType(ResponseContentType.String)
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            result = RestApiClientFactory.Create(configuration2).Invoke().Result;

            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);
        }
    }
```

It will generate this html report : 

<img src="/report1.png" alt="NContract html report">
