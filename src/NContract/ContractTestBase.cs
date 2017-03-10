using System.Threading.Tasks;
using NContract.FluentRestApi;
using NUnit.Framework;
using HttpClientFactory = NContract.FluentRestApi.HttpClientFactory;

namespace NContract
{
    [TestFixture]
    public class ContractTestBase
    {
        private ContractTestFixture _contractTestFixture;
        private ContractTest _runningTest;
        private RestApiClientFactory _restApiClientFactory;
        
        [TestFixtureSetUp]
        public void Init()
        {
            _contractTestFixture = new ContractTestFixture();
            Runner.Instance.AppendFixture(_contractTestFixture);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            _contractTestFixture.TearDown();
        }

        [SetUp]
        public virtual void Setup()
        {
            _runningTest = new ContractTest(TestContext.CurrentContext.Test.Name);
            _runningTest.SetUp();
            _contractTestFixture.AppendTest(_runningTest);
            _restApiClientFactory = new RestApiClientFactory();
        }

        [TearDown]
        public virtual void TearDown()
        {
            _runningTest.TearDown();
        }

        public Task<InvocationResult> ApiInvoke(RestApiClientConfiguration configuration)
        {
            var restApiClient = _restApiClientFactory.Create(configuration);

            var restApiInvocation = new RestApiInvocation(new HttpClientFactory(), configuration);

            _runningTest.ApiInvocations.Add(restApiInvocation);

            return restApiClient.Invoke(restApiInvocation);

        }
    }
}