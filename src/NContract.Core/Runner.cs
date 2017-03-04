using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NContract.Core
{
    public sealed class Runner
    {
        private DateTime _utcNow;
        private const string ReportFileNameFormat = @".\NContract\NContract_report_{0}.html";
        private readonly List<ContractTestFixture> _contractTestFixtures;

        public IReadOnlyCollection<ContractTestFixture> ContractTestFixtures => _contractTestFixtures.AsReadOnly();

        private static readonly Lazy<Runner> Lazy = new Lazy<Runner>(() => new Runner());

        public static Runner Instance => Lazy.Value;

        public RunningStatus RunningStatus { get; private set; }

        private Runner()
        {
            RunningStatus = RunningStatus.Started;

            _utcNow = DateTime.UtcNow;
            _contractTestFixtures = new List<ContractTestFixture>();

            Task.Run(() => WriteReport());
        }

        private async Task WriteReport()
        {
            while (RunningStatus != RunningStatus.Done)
            {
                Thread.Sleep(500);

                if (_contractTestFixtures.ToList().All(f => f.ContractTests.All(ct => ct.RunningStatus == RunningStatus.Done)))
                {
                    RunningStatus = RunningStatus.Done;
                }
            }

            WriteToFile();
        }

        private void WriteToFile()
        {
            if (!Directory.Exists("NContract"))
                Directory.CreateDirectory("NContract");

            var reportFileName = string.Format(ReportFileNameFormat, _utcNow.ToString("yyyyMMddHHmmss"));
            string title = $"NContract report";
            string reportHtml = $"<!DOCTYPE html><html><title>{title}</title><link rel=\"stylesheet\" href=\"..\\normalize.css\"><body>";
            reportHtml += $"<h1>{title}</h1>";
            reportHtml += $"<p>Ran at {_utcNow}</p>";
            reportHtml = _contractTestFixtures.Aggregate(reportHtml, (current, fixture) => current + ReportFixture(fixture));
            File.WriteAllText(reportFileName, reportHtml);
        }

        public string ReportFixture(ContractTestFixture fixture)
        {
            var  reportHtml = "<ul>";
            foreach (var test in fixture.ContractTests)
            {
                reportHtml += $"<li><h2 class=\"{test.ResultStatus.ToString().ToLower()}\">{test.Name}</h2>";
                foreach (var invoke in test.ApiInvocations)
                {
                    //reportHtml += "<p><strong>Request</string></br>";
                    //reportHtml += $"Method:{invoke.Request.HttpMethod}</br>";
                    //reportHtml += $"Uri:{invoke.Request.Uri}</br>";
                    //reportHtml += $"Body:{invoke.Request?.Body}</br>";
                    //if (invoke.Request?.Headers != null)
                    //    foreach (var header in invoke.Request.Headers)
                    //        reportHtml += $"\t{header.Key}:{header.Value}</br>";
                    //reportHtml += "</p>";
                    //reportHtml += "<p><strong>Response</strong></br>";
                }
                reportHtml += $"<tr><td>Result:{test.ResultStatus}</br>";
                reportHtml += $"<tr><td>Execution time:{test.ExecutionTime.ToString("h'h 'm'm 's's'")}</p>";
            }
            reportHtml += "</ul><br/>";
            return reportHtml;
        }

        public void AppendFixture(ContractTestFixture fixture)
        {
            _contractTestFixtures.Add(fixture);
        }
    }
}