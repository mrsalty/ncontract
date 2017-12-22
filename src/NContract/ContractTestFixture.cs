using System;
using System.Collections.Generic;

namespace NContract
{
    public class ContractTestFixture
    {
        private readonly List<ContractTestBase> _contractTests;

        public IReadOnlyCollection<ContractTestBase> ContractTests => _contractTests.AsReadOnly();

        public RunningStatus RunningStatus { get; set; }

        public Guid Id { get; }

        public ContractTestFixture()
        {
            Id = Guid.NewGuid();
            RunningStatus = RunningStatus.Started;
            _contractTests = new List<ContractTestBase>();
        }

        public void AppendTest(ContractTestBase test)
        {
            _contractTests.Add(test);
        }

        public void TearDown()
        {
            RunningStatus = RunningStatus.Done;
            ContractTestContext.Instance.WriteFixtureReport(Id);
        }
    }
}