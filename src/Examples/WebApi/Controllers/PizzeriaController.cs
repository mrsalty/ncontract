using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Domain;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("pizzeria")]
    public class PizzeriaController : ApiController
    {
        private readonly OrderService _orderService;

        public PizzeriaController()
        {
            _orderService = new OrderService();

            CreateSomeOrders();
        }

        [Route("orders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrders()
        {
            return Ok(await _orderService.GetOrderList());
        }

        [Route("orders")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody]AddOrderRequest request)
        {
            try
            {
                var order = await _orderService.AddOrder(new Order(request.TableId, request.Pizzas));
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new ObjectContent(typeof(AddOrderReponse), order, new JsonMediaTypeFormatter()
                    {
                        Indent = true,
                        SerializerSettings = new JsonSerializerSettings()
                        {
                            DateFormatString = "g",
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                    })
                });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
        }

        [Route("orders/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _orderService.GetOrderDetails(id));

            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.PreconditionFailed));
            }
        }

        [Route("orders/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri]Guid id, [FromBody]List<Pizza> pizzas)
        {
            var updated = await _orderService.UpdateOrder(id, pizzas);

            if (updated)
                return Ok();

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.PreconditionFailed));
        }

        [Route("orders/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri]Guid id)
        {
            var deleted = await _orderService.DeleteOrder(id);

            if (deleted)
                return Ok();

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.PreconditionFailed));
        }

        private async Task CreateSomeOrders()
        {
            await _orderService.AddOrder(new Order(Guid.NewGuid(), new List<Pizza>()
            {
                new Pizza(PizzaType.Margherita),
                new Pizza(PizzaType.Marinara)
            }));

            await _orderService.AddOrder(new Order(Guid.NewGuid(), new List<Pizza>()
            {
                new Pizza(PizzaType.Diavola),
                new Pizza(PizzaType.QuattroStagioni),
                new Pizza(PizzaType.Napoli)
            }));

            await _orderService.AddOrder(new Order(Guid.NewGuid(), new List<Pizza>()
            {
                new Pizza(PizzaType.Diavola),
                new Pizza(PizzaType.Marinara),
                new Pizza(PizzaType.Napoli),
                new Pizza(PizzaType.Marinara),
                new Pizza(PizzaType.Marinara)
            }));
        }
    }
}
