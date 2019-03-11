using Akka.Actor;
using Domain.Examples.Domains.Order.Commands;
using Domain.Examples.Domains.Order.Models;
using Domain.Examples.Events;
using System;
using System.Linq;

namespace Domain.Examples.Aggregates
{
    public class OrderRootAggregate : ReceiveActor
    {
        private Order Order { get; }

        public OrderRootAggregate()
        {
            Order = new Order(Guid.NewGuid().ToString());
            Receive<AddProductToOrder>(m => Handle(m));
            Receive<CompleteOrder>(m => Handle(m));
        }

        public void Handle(AddProductToOrder msg)
        {
            Order.Products.Add(new OrderedProduct(msg.ProductId));
        }

        public void Handle(CompleteOrder message)
        {
            var prodcutsIds = Order.Products
                .Select(p => p.Id)
                .ToArray();

            Context.System.EventStream.Publish(new OrderCompleted(Order.Id, prodcutsIds));
        }
    }
}
