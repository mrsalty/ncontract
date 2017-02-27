using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using log4net;
using Newtonsoft.Json.Serialization;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfiguration
    {
        public static void SetHttpConfiguration()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Formatters.Where(x => x.GetType() != typeof(JsonMediaTypeFormatter))
                         .ToList()
                         .ForEach(x => GlobalConfiguration.Configuration.Formatters.Remove(x));

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
