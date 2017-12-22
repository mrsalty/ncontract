using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NContract
{
    public abstract class ContractTestBase
    {
        private readonly Stopwatch _stopwatch;

        protected ContractTestBase(string name)
        {
            Id = Guid.NewGuid();
            RunningStatus = RunningStatus.None;
            ApiInvocations = new List<RestApiInvocation>();
            Name = name;
            _stopwatch = new Stopwatch();
        }

        public Guid Id { get; }

        public TimeSpan ExecutionTime { get; private set; }

        public DateTime ExecutionStart { get; private set; }

        public DateTime ExecutionEnd { get; private set; }

        public string Name { get; }

        public IList<RestApiInvocation> ApiInvocations { get; }

        public ContractTestResultStatus ResultStatus { get; protected set; }

        public RunningStatus RunningStatus { get; private set; }
        
        public virtual void SetUp()
        {
            ExecutionStart = DateTime.UtcNow;
            _stopwatch.Start();
            RunningStatus = RunningStatus.Started;
        }

        public virtual void TearDown()
        {
            ExecutionEnd = DateTime.UtcNow;
            _stopwatch.Stop();
            RunningStatus = RunningStatus.Done;
            ExecutionTime = _stopwatch.Elapsed;
            _stopwatch.Reset();
        }

        public virtual Task<InvocationResult> InvokeApiAsync(RestApiClientConfiguration configuration)
        {
            var restApiClient = new RestApiClient(configuration);
            var restApiInvocation = new RestApiInvocation(configuration);
            ApiInvocations.Add(restApiInvocation);
            return restApiClient.Invoke(restApiInvocation);
        }
    }
}
