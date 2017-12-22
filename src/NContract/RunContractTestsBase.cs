using System.Threading.Tasks;

namespace NContract
{
    public abstract class RunContractTestsBase : IRunContractTests
    {
        protected RunContractTestsBase()
        {
            FixtureSetUp();
        }

        public ContractTestFixture ContractTestFixture { get; private set; }

        public ContractTestBase RunningTest { get;  set; }

        public virtual void FixtureSetUp()
        {
            ContractTestFixture = new ContractTestFixture();
            ContractTestContext.Instance.AppendFixture(ContractTestFixture);
        }

        public virtual void FixtureTearDown()
        {
            ContractTestFixture.TearDown();
        }

        public virtual void TestSetup()
        { }

        public virtual void TestTearDown()
        {
            RunningTest.TearDown();
        }

        public async Task<InvocationResult> InvokeApiAsync(RestApiClientConfiguration configuration)
        {
            return await RunningTest.InvokeApiAsync(configuration);
        }
    }
}