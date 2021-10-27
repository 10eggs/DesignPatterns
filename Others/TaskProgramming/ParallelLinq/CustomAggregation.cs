using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Others.TaskProgramming.ParallelLinq
{
    public class CustomAggregation
    {
        public static void Demo()
        {
            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                0,
                (partialSum, i) => partialSum + i,
                (total,subtotals)=> total+=subtotals,
                //What to do with the final result - in this case just return it
                i => i);

            Console.WriteLine($"Sum is {sum}");

        }
    }
}
