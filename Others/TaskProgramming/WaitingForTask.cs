using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming
{
    public class WaitingForTask
    {
        public static void CheckWaitingForTask()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("I take five seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i} iteration");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done");
            }, token);

            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);
            //Task.WaitAny(t, t2);
            //Task.WaitAny(new[] { t, t2 },1000);
            //Task.WaitAll(t);
            //t.Wait();


            //if we will Cancel the token then WaitAll will throw
            //Console.ReadKey();
            //cts.Cancel();


            Task.WaitAll(new[] { t, t2 }, 4000, token);

            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            Console.WriteLine("Task has been finished");
        }
    }
}
