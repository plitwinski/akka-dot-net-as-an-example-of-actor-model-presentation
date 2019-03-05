using Akka.Actor;
using Data.Streams.Actors;
using Data.Streams.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Streams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var stream = GetStream();

            var actorSystem = ActorSystem.Create("DataStreams");

            foreach (var msg in stream)
            {
                var actor = actorSystem.ActorOf(Props.Create<StreamDigester>());
                actor.Tell(msg);
            }

            Console.ReadKey();
            await actorSystem.Terminate();
        }

        private static IEnumerable<PriceChangedMessage> GetStream()
        {
            const int streamSize = 10;

            var stream = Enumerable.Range(1, streamSize)
                .Select(p => new PriceChangedMessage() { NewPrice = p * 10 });
            return stream;
        }
    }
}
