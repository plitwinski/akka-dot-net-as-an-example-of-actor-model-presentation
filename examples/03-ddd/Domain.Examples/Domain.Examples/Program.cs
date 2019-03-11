using Akka.Actor;
using Domain.Examples.Aggregates;
using Domain.Examples.Constants;
using Domain.Examples.Domains.Order.Commands;
using Domain.Examples.Domains.Warehouse;
using Domain.Examples.Domains.Warehouse.Commands;
using Domain.Examples.Events;
using System;
using System.Threading.Tasks;

namespace Domain.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var system = ActorSystem.Create("Shopping");

            var warehouse1 = CreateWarehouse(system);
            warehouse1.Tell(new AddProductToWarehouse(ProductIds.ChocolateBar, 3));

            var warehouse2 = CreateWarehouse(system);
            warehouse2.Tell(new AddProductToWarehouse(ProductIds.Biscuit, 4));

            CreateChocolateBarOrder(system);
            CreateBiscuitOrder(system);

            Console.WriteLine("Press any key to get warehouses' statuses");
            Console.ReadKey();

            var warehouse1Status = await warehouse1.Ask<WarehouseStatusCompleted>(new GetWarehouseStatus());
            Console.WriteLine(warehouse1Status.Report);

            var warehouse2Status = await warehouse2.Ask<WarehouseStatusCompleted>(new GetWarehouseStatus());
            Console.WriteLine(warehouse2Status.Report);

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }

        private static void CreateBiscuitOrder(ActorSystem system)
        {
            Console.WriteLine("Press any key to create biscuit order");
            Console.ReadKey();

            var biscuitOrder = system.ActorOf(Props.Create<OrderRootAggregate>());
            biscuitOrder.Tell(new AddProductToOrder(ProductIds.Biscuit));
            biscuitOrder.Tell(new CompleteOrder());
        }

        private static void CreateChocolateBarOrder(ActorSystem system)
        {
            Console.WriteLine("Press any key to create chocolate bar order");
            Console.ReadKey();

            var chocolateBarOrder = system.ActorOf(Props.Create<OrderRootAggregate>());
            chocolateBarOrder.Tell(new AddProductToOrder(ProductIds.ChocolateBar));
            chocolateBarOrder.Tell(new AddProductToOrder(ProductIds.ChocolateBar));
            chocolateBarOrder.Tell(new CompleteOrder());
        }

        private static IActorRef CreateWarehouse(ActorSystem system)
        {
            var warehouse = system.ActorOf(Props.Create<WarehouseRootAggregate>());
            system.EventStream.Subscribe(warehouse, typeof(OrderCompleted));
            return warehouse;
        }
    }
}
