using System.Collections.Generic;

namespace NContract.Core
{
    public class ContractTestFixture
    {
        private readonly List<ContractTest> _contractTests;

        public IReadOnlyCollection<ContractTest> ContractTests => _contractTests.AsReadOnly();
        public RunningStatus RunningStatus { get; set; }

        public ContractTestFixture()
        {
            RunningStatus = RunningStatus.Started;
            _contractTests = new List<ContractTest>();
        }

        public void AppendTest(ContractTest test)
        {
            _contractTests.Add(test);
        }

        public void TearDown()
        {
            RunningStatus = RunningStatus.Done;
        }
    }
}