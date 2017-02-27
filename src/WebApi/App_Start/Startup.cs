using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using Castle.Windsor;
using log4net;
using log4net.Config;
using Microsoft.Owin.Cors;
using Owin;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public void Configuration(IAppBuilder app)
        {
            RegisterOwinCors(app);
            XmlConfigurator.Configure();
            
            WebApiConfiguration.SetHttpConfiguration();
            
            _logger.InfoFormat("WebApi.IISHost starting version {0}", GetVersion());

            LogConfigurationSettings();

            app.UseWebApi(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        private void LogConfigurationSettings()
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                _logger.InfoFormat("Configuration setting key=[{0}], value=[{1}]", key, ConfigurationManager.AppSettings[key]);
            }
        }

        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.FileVersion;
        }

        private void RegisterOwinCors(IAppBuilder app)
        {
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true
            };

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });
        }
    }
}