using Akka.Actor;
using Domain.Examples.Domains.Warehouse.Commands;
using Domain.Examples.Events;
using System;
using System.Linq;

namespace Domain.Examples.Domains.Warehouse
{
    public class WarehouseRootAggregate : ReceiveActor
    {
        private Warehouse Warehouse { get; }

        public WarehouseRootAggregate()
        {
            Warehouse = new Warehouse(Guid.NewGuid().ToString());
            Receive<AddProductToWarehouse>(m => Handle(m));
            Receive<OrderCompleted>(m => Handle(m));
            Receive<GetWarehouseStatus>(m => Handle(m));
        }

        public void Handle(AddProductToWarehouse msg)
        {
            var existingProduct = Warehouse.Products.FirstOrDefault(x => x.Id == msg.Id);
            if(existingProduct == null)
            {
                existingProduct = new WarehouseProduct(msg.Id);
                Warehouse.Products.Add(existingProduct);
            }
            existingProduct.Quantity += msg.Quantity;
        }

        public void Handle(OrderCompleted msg)
        {
            var soldProducts = msg.SoldProducts
                .GroupBy(k => k)
                .Select(p => new
                {
                    ProductId = p.Key,
                    Count = p.Count()
                });

            foreach (var soldProduct in soldProducts)
            {
                var existingProduct = Warehouse.Products.FirstOrDefault(x => x.Id == soldProduct.ProductId);
                if (existingProduct == null)
                {
                    Console.WriteLine($"WARN: Product id {soldProduct.ProductId} does not exist in Warehouse {Warehouse.Name}");
                    continue;
                }
                existingProduct.Quantity -= soldProduct.Count;
            }
        }

        public void Handle(GetWarehouseStatus msg)
        {
            var report = $@"Warehouse {Warehouse.Name} contains following products:
{string.Join(Environment.NewLine, Warehouse.Products.Select(p => $"- {p.Id}: {p.Quantity}" ))}";

            Sender.Tell(new WarehouseStatusCompleted(report));
        }
    }
}
