using System;
using System.IO;
using System.Linq;

namespace NContract.Core
{
    public class ReportTextWriter
    {
        private readonly DateTime _utcNow;
        private const string ReportFileNameFormat = @".\NContract\report_{0}.html";
        private readonly string _reportFileName;

        public ReportTextWriter()
        {
            _utcNow = DateTime.UtcNow;
            if (!Directory.Exists("NContract"))
                Directory.CreateDirectory("NContract");
            _reportFileName = string.Format(ReportFileNameFormat, _utcNow.ToString("yyyyMMddHHmmss"));
        }

        public void WriteHeader()
        {
            string title = $"NContract report";
            string reportHtml = $"<!DOCTYPE html><html><title>{title}</title><link rel=\"stylesheet\" href=\"..\\normalize.css\"><body>";
            reportHtml += $"<h1>{title}</h1>";
            reportHtml += $"<p>Ran at {_utcNow}</p>";
            File.AppendAllText(_reportFileName, reportHtml);
        }

        public void WriteFixtureReport(Guid fixtureId)
        {
            var fixture = Runner.Instance.ContractTestFixtures.SingleOrDefault(x => x.Id == fixtureId);

            if (fixture != null)
            {
                var reportHtml = "<ul>";
                foreach (var test in fixture.ContractTests)
                {
                    reportHtml += $"<li><h2 class=\"{test.ResultStatus.ToString().ToLower()}\">{test.Name}</h2>";
                    foreach (var invocation in test.ApiInvocations)
                    {
                        reportHtml += "<p><strong>Request</string></br>";
                        reportHtml += $"Method:{invocation.InvocationResult.RestApiClientConfiguration.HttpMethod}</br>";
                        reportHtml += $"Uri:{invocation.InvocationResult.RestApiClientConfiguration.RequestUri}</br>";
                        reportHtml += $"Body:{invocation.InvocationResult.RestApiClientConfiguration.Model}</br>";
                        if (invocation.InvocationResult.RestApiClientConfiguration.Headers != null)
                            reportHtml += $"\t{invocation.InvocationResult.RestApiClientConfiguration.Headers}</br>";
                        reportHtml += "</p>";
                        reportHtml += "<p><strong>Response</strong></br>";
                    }
                    reportHtml += $"<tr><td>Result:{test.ResultStatus}</br>";
                    reportHtml += $"<tr><td>Execution time:{test.ExecutionTime:h'h 'm'm 's's'}</p>";
                }
                reportHtml += "</ul><br/>";
                File.AppendAllText(_reportFileName, reportHtml);
            }
        }
    }
}