using System.Threading.Tasks;
using NUnit.Framework;

namespace NContract.Nunit
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

        public async Task<InvocationResult> InvokeApiAsync(RestApiClientConfiguration configuration)
        {
            return await _runningTest.InvokeApiAsync(configuration);
        }
    }
}