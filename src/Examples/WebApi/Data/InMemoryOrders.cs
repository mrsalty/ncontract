using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Domain;

namespace WebApi.Data
{
    public sealed class InMemoryOrders : List<Order>
    {
        private static readonly Lazy<InMemoryOrders> lazy =
            new Lazy<InMemoryOrders>(() => new InMemoryOrders());

        public static InMemoryOrders Instance { get { return lazy.Value; } }

        private InMemoryOrders()
        {
        }
    }
}