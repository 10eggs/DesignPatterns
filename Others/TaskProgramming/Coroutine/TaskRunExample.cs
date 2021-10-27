using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.Coroutine
{
    public class TaskRunExample
    {
        public async static void Demo()
        {
            var result = Task.Run(async() =>
            {
                Console.WriteLine("Task with delay 2000 started");
                await Task.Delay(2000);
                Console.WriteLine("Task with delay 2000 finished");
                return 2;
            });

            var result2 = Task.Run(() =>
            {
                Console.WriteLine("Task with delay 4000 started");
                Thread.Sleep(4000);
                Console.WriteLine("Task with delay 4000 finished");
                return 6;
            });

            var result3 = await Task.Run(async() =>
            {
                Console.WriteLine("Task number 3");
                await Task.Delay(6000);
                Console.WriteLine("Waiting time in t3 is finished");
                return "Something";

            });


            Task.WaitAll(result, result2);
            Console.WriteLine("After wait");
        }
    }
}
