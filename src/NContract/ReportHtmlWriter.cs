using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NContract.FluentRestApi;
using Newtonsoft.Json;

namespace NContract
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
            string reportHtml = $"<!DOCTYPE html><html><title>{title}</title><style>{Css()}</style><body>";
            reportHtml += $"<h1>{title}</h1>";
            reportHtml += $"<p>Run at {_runnintTimeUtc:O}</p>";
            File.AppendAllText(_reportFileName, reportHtml);
        }

        public void WriteFixtureReport(Guid fixtureId)
        {
            var fixture = Runner.Instance.ContractTestFixtures.SingleOrDefault(x => x.Id == fixtureId);

            if (fixture != null)
            {
                //var reportHtml = new StringBuilder();
                //reportHtml.ToString()
                var reportHtml = "<section>";
                foreach (var test in fixture.ContractTests)
                {
                    reportHtml += "<article><details>";
                    reportHtml += $"<summary class=\"{test.ResultStatus.ToString().ToLower()}\">{test.Name}</summary>";
                    reportHtml += "<h5>Api invocations</h5>";
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
                            reportHtml += $"Body:<PRE class=\"code-json\">{JsonConvert.SerializeObject(invocation.InvocationResult.RestApiClientConfiguration.Model, Formatting.Indented)}</PRE><br/>";
                        //headers
                        if (invocation.InvocationResult.RestApiClientConfiguration.Headers != null)
                            reportHtml += $"{Regex.Replace(invocation.InvocationResult.RestApiClientConfiguration.Headers.ToString(), @"\r\n?|\n", "<br />")}";
                        //response
                        reportHtml += "<h4>Response</h4>";
                        reportHtml += $"HttpStatus:{invocation.InvocationResult.HttpResponseMessage.StatusCode}<br/>";
                        if (invocation.InvocationResult.RestApiClientConfiguration.ResponseContentType == ResponseContentType.String)
                            reportHtml += $"Content:<PRE class=\"code-json\">{JsonConvert.SerializeObject(invocation.InvocationResult.StringContent, Formatting.Indented)}</PRE><br/>";
                        reportHtml += $"Invocation reponse time:{invocation.InvocationResult.InvocationTime:c}ms<br/>";
                        if (invocation.InvocationResult.Exceptions.Any())
                        {
                            reportHtml += "<h4>Exceptions</h4>";
                            reportHtml = invocation.InvocationResult.Exceptions.Aggregate(reportHtml, (current, exception) => current + $"{exception}<br />");
                        }

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

        public string Css()
        {
            return @"

html {
  font-family:Courier, Times, serif;
  line-height: 1.15; 
  -ms-text-size-adjust: 100%; 
  -webkit-text-size-adjust: 100%; 
}
body {
  margin: 10;
}
article,
aside,
footer,
header,
nav,
section {
  display: block;
}
h1 {
  font-size: 2em;
  margin: 0.67em 0;
}

h2, h3, h4, h5 {
    font-weight: bold;
    margin: 5px 0 0 0;
    padding: 0;
}
ol {
    margin-left:5px;
    margin-top: 5px;
    padding: 0;
}
li {
    margin: 0 0 0 15px;
    padding: 5px;
}
.code-json {
    background-color: antiquewhite;
    padding : 3px;
    font-size: 1.0em;   
}
.passed {
    font-size: 0.8em;
    background-color: forestgreen;
}
.failed {
    font-size: 0.8em;
    background-color: red;
}
.ignored {
    font-size: 0.8em;
    background-color: yellow;
}
summary {
    font-weight: bold;
}
details {
    padding: 0.1em;
}
.invocation-item {    
    font-size: 0.7em;
}
.fixture-results {
    font-size: 0.7em;    
}
.invocation-title {
    text-decoration: underline;
}";
        }
    }
}