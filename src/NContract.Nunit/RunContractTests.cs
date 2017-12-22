using NUnit.Framework;

namespace NContract.NUnit
{
    [TestFixture]
    public class RunContractTests : RunContractTestsBase
    {
        [OneTimeSetUp]
        public override void FixtureSetUp()
        {
            base.FixtureSetUp();
        }

        [OneTimeTearDown]
        public override void FixtureTearDown()
        {
            base.FixtureTearDown();
        }

        [SetUp]
        public override void TestSetup()
        {
            RunningTest = new ContractTest(TestContext.CurrentContext.Test.Name);
            RunningTest.SetUp();
            ContractTestFixture.AppendTest(RunningTest);
        }

        [TearDown]
        public override void TestTearDown()
        {
            base.TestTearDown();
        }
    }
}