using System.Collections.Generic;

namespace Domain.Examples.Domains.Order.Models
{
    public class Order
    {
        public Order(string id)
        {
            Id = id;
            Products = new List<OrderedProduct>();
        }

        public string Id { get; }
        public IList<OrderedProduct> Products { get; set; }
    }
}
