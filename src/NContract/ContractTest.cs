using System;
using System.Collections.Generic;
using System.Diagnostics;
using NContract.FluentRestApi;
using NUnit.Framework;

namespace NContract
{
    public class ContractTest
    {
        private readonly Stopwatch _stopwatch;

        public ContractTest(string name)
        {
            Id = Guid.NewGuid();
            RunningStatus = RunningStatus.None;
            ApiInvocations = new List<RestApiInvocation>();
            Name = name;
            _stopwatch = new Stopwatch();
        }

        public Guid Id { get; private set; }

        public TimeSpan ExecutionTime { get; private set; }

        public DateTime ExecutionStart { get; private set; }

        public DateTime ExecutionEnd { get; private set; }

        public string Name { get; private set; }

        public IList<RestApiInvocation> ApiInvocations { get; private set; }

        public ContractTestResultStatus ResultStatus { get; private set; }

        public RunningStatus RunningStatus { get; private set; }


        public void SetUp()
        {
            ExecutionStart = DateTime.UtcNow;
            _stopwatch.Start();
            RunningStatus = RunningStatus.Started;
        }

        public void TearDown()
        {
            ExecutionEnd = DateTime.UtcNow;
            _stopwatch.Stop();
            RunningStatus = RunningStatus.Done;
            ExecutionTime = _stopwatch.Elapsed;
            _stopwatch.Reset();
            ResultStatus = TestContext.CurrentContext.Result.Status.ToContractTestStatus();
        }
    }
}
