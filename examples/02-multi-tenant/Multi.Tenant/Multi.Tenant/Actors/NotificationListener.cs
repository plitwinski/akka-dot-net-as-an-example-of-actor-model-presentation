using Akka.Actor;
using Multi.Tenant.Apis;
using Multi.Tenant.Messages;
using System;

namespace Multi.Tenant.Actors
{
    public class NotificationListener : ReceiveActor
    {
        public NotificationListener()
        {
            Idle();
        }

        public void Idle()
        {
            Receive<StartListening>(msg =>
            {
                Console.WriteLine($"Start processing for user id {msg.UserId}");
                Become(Working);
                Self.Tell(new ProcessNotification() { UserId = msg.UserId }, Sender);
            });

            Receive<StopListening>(msg => 
                Console.WriteLine($"Cannot stop processing for user id {msg.UserId} as listening has not started yet"));
        }

        private void Working()
        {
            Receive<StartListening>(msg => 
                Console.WriteLine($"Work has already started for user id {msg.UserId}"));

            Receive<ProcessNotification>(msg =>
            {
                NotificationApi.GetNotificationAsync(msg.UserId)
                    .ContinueWith(task =>
                    {
                        Console.WriteLine($"Received: {task.Result.Text}");
                        return new ProcessNotification() { UserId = msg.UserId };
                    }).PipeTo(Self);
            });

            Receive<StopListening>(msg =>
            {
                Become(Idle);
                Console.WriteLine($"Stopped listening for {msg.UserId}");
            });
        }
    }
}
