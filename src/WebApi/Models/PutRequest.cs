using System;

namespace WebApi.Models
{
    public class PutRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}