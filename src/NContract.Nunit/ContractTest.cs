using NUnit.Framework;

namespace NContract.NUnit
{
    public class ContractTest : ContractTestBase
    {
        public ContractTest(string name) : base(name)
        {
        }

        public override void TearDown()
        {
            base.TearDown();
            ResultStatus = TestContext.CurrentContext.Result.Outcome.Status.ToContractTestStatus();
        }
    }
}