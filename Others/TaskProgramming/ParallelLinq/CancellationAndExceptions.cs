using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DesignPatterns.Others.TaskProgramming.ParallelLinq
{
    public class CancellationAndExceptions
    {
        public static void Demo()
        {
            var cts = new CancellationTokenSource();
            var items = ParallelEnumerable.Range(1, 20);
            var results = items.WithCancellation(cts.Token).Select(x =>
            {
                double result = Math.Log10(x);
                Console.WriteLine($"I value = {x}");

                //if (result > 1)
                //{
                //    throw new InvalidOperationException();
                //}
                return result;
            });


            try
            {
                foreach (var i in results)
                {
                    if (i > 1) cts.Cancel();
                    Console.WriteLine($"Result = {i}");
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => { Console.WriteLine($"{e.GetType().Name}:{e.Message}");
                    return true;
                });
            }
            catch(OperationCanceledException oce)
            {
                Console.WriteLine($"Cancelled");
            }
        }
    }
}
