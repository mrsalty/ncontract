using System.Threading.Tasks;

namespace NContract
{
    public interface IRunContractTests
    {
        void FixtureSetUp();

        void FixtureTearDown();

        void TestSetup();

        void TestTearDown();

        Task<InvocationResult> InvokeApiAsync(RestApiClientConfiguration configuration);
    }
}