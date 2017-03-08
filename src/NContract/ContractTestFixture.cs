using System;
using System.Collections.Generic;

namespace NContract
{
    public class ContractTestFixture
    {
        private readonly List<ContractTest> _contractTests;

        public IReadOnlyCollection<ContractTest> ContractTests => _contractTests.AsReadOnly();
        public RunningStatus RunningStatus { get; set; }

        public Guid Id { get; }

        public ContractTestFixture()
        {
            Id = Guid.NewGuid();
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
            Runner.Instance.WriteFixtureReport(Id);
        }
    }
}