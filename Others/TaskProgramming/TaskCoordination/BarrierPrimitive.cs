using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{
    public class BarrierPrimitive
    {
        //**args**
        //first - number of threads
        //second - handler for what happens if phase is finished
        public static Barrier barrier = new Barrier(2, b=>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Kettle, takes a bit longer");
            Thread.Sleep(5000);
            //Counter is 'signaled' here - if it was originally 1, then it is increased by one and waiting
            barrier.SignalAndWait();
            Console.WriteLine("Pouring water into the cup"); // count is reset to 0, again
            barrier.SignalAndWait();
            Console.WriteLine("Putting the kettle away");

        }

        public static void Cup()
        {
            Console.WriteLine("Proper cup for tea(quick one)");
            barrier.SignalAndWait();
            Console.WriteLine("Adding tea");
            barrier.SignalAndWait();
            Console.WriteLine("Adding sugar");
        }
        
        public static void Demo()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, t =>
            {
                Console.WriteLine("When water and cup are ready we can finally enjoy our tea !");
            });

            tea.Wait();
        }
    }
}
