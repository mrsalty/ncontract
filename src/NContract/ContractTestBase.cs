using System.Threading.Tasks;
using NContract.FluentRestApi;
using NUnit.Framework;

namespace NContract
{
    [TestFixture]
    public class ContractTestBase
    {
        private ContractTestFixture _contractTestFixture;
        private ContractTest _runningTest;

        [OneTimeSetUp]
        public void Init()
        {
            _contractTestFixture = new ContractTestFixture();
            Runner.Instance.AppendFixture(_contractTestFixture);
        }

        [OneTimeTearDown]
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
        }

        [TearDown]
        public virtual void TearDown()
        {
            _runningTest.TearDown();
        }

        public Task<InvocationResult> InvokeApi(RestApiClientConfiguration configuration)
        {
            return _runningTest.InvokeApi(configuration);
        }
    }
}