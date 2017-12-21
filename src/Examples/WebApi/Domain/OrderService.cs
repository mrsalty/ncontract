using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Domain
{
    public class OrderService
    {
        public async Task<dynamic> AddOrder(Order order)
        {
            return await Task.Run(() =>
            {
                if (InMemoryOrders.Instance.Any(o => o.TableId == order.TableId))
                    throw new InvalidOperationException($"order already created for table with id {order.TableId}");

                 InMemoryOrders.Instance.Add(order);

                 return new AddOrderReponse
                 {
                     OrderId = order.OrderId,
                     OrderDateTime = order.OrderDateTime
                 };
             });
        }

        public async Task<List<GetOrderDetailsResponse>> GetOrderList()
        {
            return await Task.Run(() =>
            {
                return InMemoryOrders.Instance.Select(item => new GetOrderDetailsResponse()
                {
                    OrderDateTime = item.OrderDateTime,
                    OrderId = item.OrderId,
                    Pizzas = item.Pizzas
                }).ToList();
            });
        }

        public async Task<GetOrderDetailsResponse> GetOrderDetails(Guid orderId)
        {
            return await Task.Run(() =>
            {
                var order = InMemoryOrders.Instance.SingleOrDefault(o => o.OrderId == orderId);

                if (order == null)
                {
                    throw new InvalidOperationException($"Order with id={orderId} not found");
                }

                return new GetOrderDetailsResponse()
                {
                    OrderDateTime = order.OrderDateTime,
                    OrderId = order.OrderId,
                    Pizzas = order.Pizzas
                };
            });
        }

        public async Task<bool> UpdateOrder(Guid orderId, List<Pizza> pizzas)
        {
            return await Task.Run(() =>
            {
                var order = InMemoryOrders.Instance.SingleOrDefault(o => o.OrderId == orderId);

                if (order == null)
                    return false;

                order.UpdatePizzas(pizzas);
                return true;
            });
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            return await Task.Run(() =>
            {
                var order = InMemoryOrders.Instance.SingleOrDefault(o => o.OrderId == orderId);

                if (order == null)
                    return false;

                InMemoryOrders.Instance.Remove(order);
                return true;
            });
        }
    }
}
