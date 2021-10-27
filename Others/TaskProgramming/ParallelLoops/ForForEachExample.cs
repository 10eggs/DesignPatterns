using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ParallelLoops
{
    public class ForForEachExample
    {
        public static void Demo()
        {
            int[] values = new int[100];
            for(int i = 0; i < 100; i++)
            {

            }
        }

        public static void ProcessActions()
        {
            var a = new Action(() => Console.WriteLine($"First action on thread {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second action on thread {Task.CurrentId}"));
            var c = new Action(() => {
                Console.WriteLine($"Third action on thread {Task.CurrentId}");
                Thread.Sleep(4000);
            });

            //All of the opereations are blocking
            Parallel.Invoke(a, b, c);
            Console.WriteLine("After Parallel invocation");
        }

        public static void ProcessForLoop()
        {
            //Step for 'FOR' loop is always 1 - but we can adjust it by using foreach (look at RANGE method)
            Parallel.For(1, 11, i =>
            {
                Console.WriteLine($"{i * i}\t");
            });
        }

        public static IEnumerable<int> Range(int start,int end,int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }

        public static void ProcessForLoopWithCustomizedStep()
        {
            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }

        public static void ProcessForeachLoop()
        {
            string[] words = { "oh", "my", "god", "what", "a", "match" };
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length {word.Length}, and it is printed on {Task.CurrentId}");
            });
        }
    }
}
