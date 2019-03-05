using Akka.Actor;
using Multi.Tenant.Messages;

namespace Multi.Tenant.Actors
{
    public class NotificationManager : ReceiveActor
    {
        public NotificationManager()
        {
            Receive<StartListening>(msg => {
                var child = GetChildByUserId(msg.UserId);
                child.Forward(msg);
            });

            Receive<StopListening>(msg => {
                var child = GetChildByUserId(msg.UserId);
                child.Forward(msg);
            });
        }

        private IActorRef GetChildByUserId(string userId)
        {
            var existingChild = Context.Child(userId);
            if(existingChild != ActorRefs.Nobody)
            {
                return existingChild;
            }

            return Context
                .ActorOf(Props.Create<NotificationListener>(), userId);
        }
    }
}
