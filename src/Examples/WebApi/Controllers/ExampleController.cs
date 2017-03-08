using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using log4net;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ExampleController : ApiController
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Route("version")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetVersionAsync()
        {
            return await Task.Run(
                () => Ok("1.0.0.0"));
        }

        [Route("header")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWithHeadersAsync()
        {
            if (Request.Headers.Accept.All(x => x.MediaType != "application/json"))
            {
                return await Task.Run(() => BadRequest());
            };

            return await Task.Run(() => Ok());
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CreateAsync(CreateRequest request)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(request?.Name))
                {
                    return BadRequest("Invalid request");
                }

                dynamic response = new JObject();
                response.id = Guid.NewGuid();
                response.name = request.Name;
                return Ok(response);
            });
        }

        [Route("put")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PutAsync(PutRequest request)
        {
            return await Task.Run(() =>
            {
                if (request?.Id == Guid.Empty)
                {
                    return BadRequest("Invalid request");
                }

                dynamic response = new JObject();
                response.id = request.Id;
                response.name = "entity updated";
                return Ok(response);
            });
        }
    }
}