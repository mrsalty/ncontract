using System;
using System.Collections.Generic;

namespace WebApi.Domain
{
    public class Order
    {

        public Guid TableId { get; }

        public Guid OrderId { get; }

        public List<Pizza> Pizzas { get; private set; }

        public DateTime OrderDateTime { get; private set; }

        public Order(Guid tableId, List<Pizza> pizzas)
        {
            OrderId = Guid.NewGuid();
            OrderDateTime = DateTime.UtcNow;
            TableId = tableId;
            Pizzas = pizzas;
        }

        public void UpdatePizzas(List<Pizza> pizzas)
        {
            OrderDateTime = DateTime.UtcNow;
            Pizzas = pizzas;
        }
    }
}