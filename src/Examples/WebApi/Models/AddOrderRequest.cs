using System;
using System.Collections.Generic;
using WebApi.Domain;

namespace WebApi.Models
{
    public class AddOrderRequest
    {
        public AddOrderRequest(Guid tableId, List<Pizza> pizzas)
        {
            Pizzas = pizzas;
            TableId = tableId;
        }

        public List<Pizza> Pizzas { get; }

        public Guid TableId { get; }
    }
}