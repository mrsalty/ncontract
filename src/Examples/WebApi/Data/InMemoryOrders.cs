using System;
using System.Collections.Generic;
using WebApi.Domain;

namespace WebApi.Data
{
    public sealed class InMemoryOrders : List<Order>
    {
        private static readonly Lazy<InMemoryOrders> Lazy = new Lazy<InMemoryOrders>(() => new InMemoryOrders());

        public static InMemoryOrders Instance => Lazy.Value;

        private InMemoryOrders()
        {
        }
    }
}