using System;
using System.Collections.Generic;
using WebApi.Domain;

namespace WebApi.Models
{
    public class GetOrderDetailsResponse
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDateTime { get; set; }

        public IEnumerable<Pizza> Pizzas { get; set; }
    }
}