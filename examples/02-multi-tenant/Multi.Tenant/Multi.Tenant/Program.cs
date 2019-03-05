using Akka.Actor;
using Multi.Tenant.Actors;
using Multi.Tenant.Messages;
using System;
using System.Threading.Tasks;

namespace Multi.Tenant
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("MultiTenant");

            var notificationManager = actorSystem.ActorOf(Props.Create<NotificationManager>());

            notificationManager.Tell(new StartListening() { UserId = "1" });
            notificationManager.Tell(new StartListening() { UserId = "1" });
            notificationManager.Tell(new StartListening() { UserId = "2" });

            Console.WriteLine("Press any key to stop processing");
            Console.ReadLine();

            notificationManager.Tell(new StopListening() { UserId = "2" });
            notificationManager.Tell(new StopListening() { UserId = "2" });
            notificationManager.Tell(new StopListening() { UserId = "1" });

            Console.ReadLine();

            await actorSystem.Terminate();
        }
    }
}
