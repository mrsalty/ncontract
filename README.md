# NContract
With NContract you can hit your REST web api in a fluent manner and verify results and contracts.
NContract is built on top of NUnit so syntax and assertions of your tests will be familiar.
NContract generates an HTML report for each test run in the 'bin\NContract' folder of your test project so that you can integrate it in your CI build pipeline.

```C#
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

            var invocationResult = ApiInvoke(configureGet);

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

            var invocationResult = ApiInvoke(configurePost);

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

            var invocationResult = ApiInvoke(configurePost);

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

            invocationResult = ApiInvoke(configurePut);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);

            //Get order
            var configureGet = new RestApiClientConfigurationBuilder()
                  .WithBaseUri("http://localhost:52730")
                  .WithRequestUri($"/pizzeria/orders/{orderId}")
                  .WithContentType("application/json")
                  .WithModel(updated)
                  .WithHttpMethod(HttpMethod.Get)
                  .Build();

            invocationResult = ApiInvoke(configureGet);

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

            var invocationResult = ApiInvoke(configurePost);
            var orderId = invocationResult.Result.StringContent.orderId.Value;
            Assert.DoesNotThrow(() => Guid.Parse(orderId));

            //delete it
            var configureDelete = new RestApiClientConfigurationBuilder()
                 .WithBaseUri("http://localhost:52730")
                 .WithRequestUri($"/pizzeria/orders/{orderId}")
                 .WithContentType("application/json")
                 .WithHttpMethod(HttpMethod.Delete)
                 .Build();

            invocationResult = ApiInvoke(configureDelete);

            Assert.AreEqual(HttpStatusCode.OK, invocationResult.Result.HttpResponseMessage.StatusCode);
        }
    }
```

Running the above tests will generate this NContract report: 

<img src="/report1.png" alt="NContract html report">
