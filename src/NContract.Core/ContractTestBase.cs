using NContract.Core.RestApi;
using NUnit.Framework;

namespace NContract.Core
{
    [TestFixture]
    public class ContractTestBase
    {
        private ContractTestFixture _contractTestFixture;
        private ContractTest _runningTest;
        protected RestApiClientFactory RestApiClientFactory { get; private set; }
        
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
            RestApiClientFactory = new RestApiClientFactory(_runningTest);
        }

        [TearDown]
        public virtual void TearDown()
        {
            _runningTest.TearDown();
        }
    }
}