using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming
{
    public class TaskWaiting
    {
        public static void CheckWaitingTask()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
             
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                var cancelled =token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled? "Bomb disarmed":"BOOOM!");
            },token);

            t.Start();
            Console.WriteLine("This text has been printed after Task has been started");
            Console.ReadKey();
            cts.Cancel();

        }
    }
}
