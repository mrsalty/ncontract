# ncontract
test against your rest webapi contracts quickly!

```C#
	public class TestFixture1 : ContractTestBase
    {
        [Test]
        public void WhenInvokeGet_Ok200StatusIsReturned()
        {
            var configuration = new RestApiClientConfigurationBuilder()
                .WithBaseUri(_baseUri)
                .WithRequestUri("/version")
                .WithContentType("application/json")
                .WithResponseContentType(ResponseContentType.String)
                .WithHttpMethod(HttpMethod.Get)
                .Build();

            var result = RestApiClientFactory.Create(configuration).Invoke();

            Assert.AreEqual(HttpStatusCode.OK, result.Result.HttpResponseMessage.StatusCode);
        }
	}
```

It will generate an html report : 

<img src="/report1.png" alt="NContract html report" width="720" height="890">
