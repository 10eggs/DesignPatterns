using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ParallelLinq
{
    public class ParallelLinqIntro
    {
        public static void Demo()
        {
            int count = 50;
            var items = Enumerable.Range(1, count).ToArray();

            var results = new int[count];

            items.AsParallel().ForAll(x =>
            {
                int newValue = x * x * x;
                Console.WriteLine($"Here is a new value: {newValue} from thread {Task.CurrentId}\t");
                results[x - 1] = newValue;
            });

            foreach(var i in results)
            {
                Console.WriteLine($"From array: {i}");
            }
            Console.WriteLine();

            Console.WriteLine("*******************");
            Console.WriteLine("In order: ");
            var ordRes = items.AsParallel().AsOrdered().Select(x => x * x * x);
            foreach(var i in ordRes)
            {
                Console.WriteLine($"Ordered list item: {i}");
            }
        }
    }
}
