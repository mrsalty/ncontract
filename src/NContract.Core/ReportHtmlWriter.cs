using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NContract.Core.RestApi;

namespace NContract.Core
{
    public class ReportHtmlWriter
    {
        private readonly DateTime _runnintTimeUtc;
        private const string ReportFileNameFormat = @".\NContract\report_{0}.html";
        private readonly string _reportFileName;

        public ReportHtmlWriter(DateTime runnintTimeUtc)
        {
            _runnintTimeUtc = runnintTimeUtc;
            if (!Directory.Exists("NContract"))
                Directory.CreateDirectory("NContract");
            _reportFileName = string.Format(ReportFileNameFormat, runnintTimeUtc.ToString("yyyyMMddHHmmss"));
            WriteHeader();
        }

        private void WriteHeader()
        {
            string title = $"NContract report";
            string reportHtml = $"<!DOCTYPE html><html><title>{title}</title><link rel=\"stylesheet\" href=\"..\\style.css\"><body>";
            reportHtml += $"<h1>{title}</h1>";
            reportHtml += $"<p>Run at {_runnintTimeUtc:O}</p>";
            File.AppendAllText(_reportFileName, reportHtml);
        }

        public void WriteFixtureReport(Guid fixtureId)
        {
            var fixture = Runner.Instance.ContractTestFixtures.SingleOrDefault(x => x.Id == fixtureId);

            if (fixture != null)
            {
                var reportHtml = "<section>";
                foreach (var test in fixture.ContractTests)
                {
                    reportHtml += "<article><details>";
                    reportHtml += $"<summary class=\"{test.ResultStatus.ToString().ToLower()}\">{test.Name}</summary>";
                    reportHtml += "<ol class=\"invocation-list\" type=\"1\">";
                    foreach (var invocation in test.ApiInvocations)
                    {
                        reportHtml += "<li class=\"invocation-item\">";
                        reportHtml += $"<h3 class=\"invocation-title\">{invocation.InvocationResult.RestApiClientConfiguration.BaseUri}{invocation.InvocationResult.RestApiClientConfiguration.RequestUri}</h3>";
                        //request
                        reportHtml += "<h4>Request</h4>";
                        reportHtml += $"Method:{invocation.InvocationResult.RestApiClientConfiguration.HttpMethod}<br/>";
                        reportHtml += $"Uri:{invocation.InvocationResult.RestApiClientConfiguration.BaseUri}{invocation.InvocationResult.RestApiClientConfiguration.RequestUri}<br/>";
                        if (invocation.InvocationResult.RestApiClientConfiguration.Model != null)
                            reportHtml += $"Body:{invocation.InvocationResult.RestApiClientConfiguration.Model}<br/>";
                        //headers
                        if (invocation.InvocationResult.RestApiClientConfiguration.Headers != null)
                            reportHtml += $"{Regex.Replace(invocation.InvocationResult.RestApiClientConfiguration.Headers.ToString(), @"\r\n?|\n", "<br />")}";
                        //response
                        reportHtml += "<h4>Response</h4>";
                        reportHtml += $"HttpStatus:{invocation.InvocationResult.HttpResponseMessage.StatusCode}<br/>";
                        if (invocation.InvocationResult.RestApiClientConfiguration.ResponseContentType == ResponseContentType.String)
                            reportHtml += $"Content:{invocation.InvocationResult.StringContent}<br/>";
                        reportHtml += $"Invocation reponse time:{invocation.InvocationResult.InvocationTime:c}ms<br/>";
                        reportHtml += "</li>";
                    }
                    reportHtml += "</ol>";
                    //fixture results
                    reportHtml += "<div class=\"fixture-results\">";
                    reportHtml += "<h3>Fixture results</h3>";
                    reportHtml += $"Fixture status:{test.ResultStatus}<br/>";
                    reportHtml += $"Execution time:{test.ExecutionTime.Duration():c}";
                    reportHtml += "</div>";
                    reportHtml += "</details>";
                    reportHtml += "</article>";
                }
                reportHtml += "</section>";
                File.AppendAllText(_reportFileName, reportHtml);
            }
        }
    }
}