using System.Threading.Tasks;

namespace NContract
{
    public abstract class RunContractTestsBase : IRunContractTests
    {
        public ContractTestFixture ContractTestFixture { get; private set; }

        public ContractTestBase RunningTest { get;  set; }

        public virtual void FixtureSetUp()
        {
            ContractTestFixture = new ContractTestFixture();
            Runner.Instance.AppendFixture(ContractTestFixture);
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

        public virtual async Task<InvocationResult> InvokeApiAsync(RestApiClientConfiguration configuration)
        {
            return await RunningTest.InvokeApiAsync(configuration);
        }
    }
}