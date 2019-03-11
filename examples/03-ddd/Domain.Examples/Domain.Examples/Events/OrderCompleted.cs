using System.Collections.Generic;

namespace Domain.Examples.Events
{
    public class OrderCompleted
    {
        public OrderCompleted(string orderId, IEnumerable<string> soldProducts)
        {
            OrderId = orderId;
            SoldProducts = soldProducts;
        }

        public string OrderId { get; }
        public IEnumerable<string> SoldProducts { get; }
    }
}
