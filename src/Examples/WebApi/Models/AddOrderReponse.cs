using System;

namespace WebApi.Models
{
    public class AddOrderReponse
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDateTime { get; set; }
    }
}