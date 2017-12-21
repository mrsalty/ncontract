using System;
using System.Collections.Generic;

namespace WebApi.Domain
{
    public class Order
    {
        public Guid OrderId { get; }

        public List<Pizza> Pizzas { get; private set; }

        public DateTime OrderDateTime { get; private set; }

        public Order(List<Pizza> pizzas)
        {
            OrderId = Guid.NewGuid();
            OrderDateTime = DateTime.UtcNow;
            Pizzas = pizzas;
        }

        public void Update(List<Pizza> pizzas)
        {
            OrderDateTime = DateTime.UtcNow;
            Pizzas = pizzas;
        }
    }
}