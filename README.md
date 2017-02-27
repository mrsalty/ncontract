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

            var result = RestApiClientFactory.Create(configuration).Invoke();

            Assert.AreEqual(HttpStatusCode.OK, result.Result.HttpResponseMessage.StatusCode);
        }
```
