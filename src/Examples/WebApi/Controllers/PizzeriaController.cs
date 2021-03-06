﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("pizzeria")]
    public class PizzeriaController : Controller
    {
        private readonly OrderService _orderService;

        public PizzeriaController()
        {
            _orderService = new OrderService();

            CreateSomeOrders();
        }

        [Route("orders")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetOrderList());
        }

        [Route("orders")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrderRequest request)
        {
            var order = await _orderService.AddOrder(new Order(request.Pizzas));
            return Created(string.Empty, order);
        }

        [Route("orders/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _orderService.GetOrder(id));
        }

        [Route("orders/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]List<Pizza> pizzas)
        {
            var response = await _orderService.PutOrder(id, pizzas);
            if (response)
                return Ok(true);
            return NotFound(false);
        }

        [Route("orders/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            return Ok(await _orderService.DeleteOrder(id));
        }

        private async Task CreateSomeOrders()
        {
            await _orderService.AddOrder(new Order(new List<Pizza>()
            {
                new Pizza(PizzaType.Margherita),
                new Pizza(PizzaType.Marinara)
            }));

            await _orderService.AddOrder(new Order(new List<Pizza>()
            {
                new Pizza(PizzaType.Diavola),
                new Pizza(PizzaType.QuattroStagioni),
                new Pizza(PizzaType.Napoli)
            }));

            await _orderService.AddOrder(new Order(new List<Pizza>()
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
