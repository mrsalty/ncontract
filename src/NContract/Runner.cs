using System;
using System.Collections.Generic;

namespace NContract
{
    public sealed class Runner
    {
        private readonly List<ContractTestFixture> _contractTestFixtures;

        public DateTime RunTimeUtc { get; }

        public IReadOnlyCollection<ContractTestFixture> ContractTestFixtures => _contractTestFixtures.AsReadOnly();

        private static readonly Lazy<Runner> Lazy = new Lazy<Runner>(() => new Runner());

        public static Runner Instance => Lazy.Value;

        public RunningStatus RunningStatus { get; private set; }

        public ReportHtmlWriter ReportTextWriter { get; private set; }

        private Runner()
        {
            RunTimeUtc = DateTime.UtcNow;

            ReportTextWriter = new ReportHtmlWriter(RunTimeUtc);

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