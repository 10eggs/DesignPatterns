using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ConcurrentCollections
{
    public class ProducerConsumerPattern
    {
        static BlockingCollection<int> messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        public static CancellationTokenSource cts = new CancellationTokenSource();

        static Random random = new Random();
        
        public static void Demo()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer },cts.Token);

            }

            catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        public static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(100));
            }

        }
        public static void RunConsumer()
        {
            //As soon as somebody ask the element its going to give u this element and you can proceed your iteration effectively
            //We can have this method as a task and just block whenever there is no element in there (it will be waiting for the element)

            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}
