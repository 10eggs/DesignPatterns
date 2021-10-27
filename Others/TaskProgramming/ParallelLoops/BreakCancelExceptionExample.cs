using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ParallelLoops
{
    public class BreakCancelExceptionExample
    {
        //The variable to store the results of the execution of Parallel.For loop
        //private static ParallelLoopResult result;

        public static void ParallelLoopWithCancelation()
        {
            try
            {
                Demo();
            }
            catch (OperationCanceledException oce) {
                Console.WriteLine(oce.ToString());
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(ae.Message);
                    return true;
                });
            }
        }


        public static void Demo()
        {
            //We can use ParallelLoop result BUT there is also available to use cancellationToken
            var cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            ParallelLoopResult result = Parallel.For(0, 20, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x} - {Task.CurrentId}\t");
                if(x == 10)
                {
                    //Only new tasks will be stopped.
                    //state.Stop();

                    // Dont remember the difference 
                    //state.Break();

                    //
                    //throw new Exception();
                    cts.Cancel();
                };
            });

            Console.WriteLine();
            Console.WriteLine($"Was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest loop iteration is equal to {result.LowestBreakIteration}");
            }
    }
    }
}
