using System;
using System.Collections.Generic;

namespace NContract.Core
{
    public sealed class Runner
    {
        private readonly List<ContractTestFixture> _contractTestFixtures;

        public DateTime RunningTimeUtc { get; private set; }

        public IReadOnlyCollection<ContractTestFixture> ContractTestFixtures => _contractTestFixtures.AsReadOnly();

        private static readonly Lazy<Runner> Lazy = new Lazy<Runner>(() => new Runner());

        public static Runner Instance => Lazy.Value;

        public RunningStatus RunningStatus { get; private set; }

        public ReportHtmlWriter ReportTextWriter { get; private set; }

        private Runner()
        {
            RunningTimeUtc = DateTime.UtcNow;

            ReportTextWriter = new ReportHtmlWriter(RunningTimeUtc);

            RunningStatus = RunningStatus.Started;

            _contractTestFixtures = new List<ContractTestFixture>();
        }

        public void AppendFixture(ContractTestFixture fixture)
        {
            _contractTestFixtures.Add(fixture);
        }

        public void WriteFixtureReport(Guid fixtureId)
        {
            ReportTextWriter.WriteFixtureReport(fixtureId);
        }
    }
}