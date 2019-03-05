using Akka.Actor;
using Data.Streams.Messages;
using System;

namespace Data.Streams.Actors
{
    public class StreamDigester : ReceiveActor
    {
        public StreamDigester()
        {
            Receive<PriceChangedMessage>(msg =>
            {
                Console.WriteLine($"Processed {nameof(PriceChangedMessage)} with price € {msg.NewPrice}");
            });
        }
    }
}
