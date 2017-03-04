using System;
using System.Collections.Generic;

namespace NContract.Core
{
    public sealed class Runner
    {
        private readonly List<ContractTestFixture> _contractTestFixtures;

        public IReadOnlyCollection<ContractTestFixture> ContractTestFixtures => _contractTestFixtures.AsReadOnly();

        private static readonly Lazy<Runner> Lazy = new Lazy<Runner>(() => new Runner());

        public static Runner Instance => Lazy.Value;

        public RunningStatus RunningStatus { get; private set; }

        public ReportTextWriter ReportTextWriter { get; private set; }

        private Runner()
        {
            ReportTextWriter = new ReportTextWriter();
            ReportTextWriter.WriteHeader();

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