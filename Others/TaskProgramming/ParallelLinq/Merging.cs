using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ParallelLinq
{
    public class Merging
    {
        public static void Demo()
        {
            var numbers = Enumerable.Range(1, 20).ToArray();

            var results = numbers
                .AsParallel()
                //.WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Produced: {result}");
                return result;
            });

            foreach(var i in results)
            {
                Console.WriteLine($"Consumed: {i}");
            }
        }
    }
}
