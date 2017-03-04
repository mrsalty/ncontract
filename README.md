# ncontract
test against your rest webapi contracts quickly!

```C#
        [Test]
        public void WhenInvokeGet_Ok200StatusIsReturned()
        {
            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri("www.yourapi.com")
                .WithRequestUri("/getmesomething")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            var result = RestApiClientFactory.Create(configuration).Invoke().Result;

            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);
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
```

NContract will generate a report for each run

![Alt text](/report1.png?raw=true "NContract report example")
