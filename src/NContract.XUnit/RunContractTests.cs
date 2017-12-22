using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace NContract.XUnit
{
    public class RunContractTests : RunContractTestsBase
    {
        //public override void TestSetup()
        //{
        //    //RunningTest = new ContractTest(TestContext.CurrentContext.Test.Name);
        //    RunningTest.SetUp();
        //    ContractTestFixture.AppendTest(RunningTest);
        //}
    }

    public class ContractTestAttribute : BeforeAfterTestAttribute
    {
        private string _currentBeforeTestName;
        private string _currentAfterTestName;

        public void TestSetup()
        {}

        public override void Before(MethodInfo methodUnderTest)
        {
            ContractTestContext.Instance.ContractTestFixtures.First().AppendTest(new ContractTest(methodUnderTest.Name));
        }

        public override void After(MethodInfo methodUnderTest)
        {
            _currentAfterTestName = methodUnderTest.Name;
        }
    }

    public class ContractTest2Attribute : AfterTestFinished
    {
        public ContractTest2Attribute(ITest test, string attributeName) : base(test, attributeName)
        {
        }
    }
}