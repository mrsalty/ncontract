using System;
using System.Collections.Generic;

namespace NContract
{
    public class ContractTestContext
    {
        private static readonly Lazy<ContractTestContext> Lazy = new Lazy<ContractTestContext>(() => new ContractTestContext());

        public static ContractTestContext Instance => Lazy.Value;

        private readonly List<ContractTestFixture> _contractTestFixtures;

        private readonly Runner _runner;

        public IReadOnlyCollection<ContractTestFixture> ContractTestFixtures => _contractTestFixtures.AsReadOnly();

        public ReportHtmlWriter ReportTextWriter { get; private set; }

        public ContractTestContext()
        {
            _runner = new Runner();
            ReportTextWriter = new ReportHtmlWriter(_runner.RunTimeUtc);
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