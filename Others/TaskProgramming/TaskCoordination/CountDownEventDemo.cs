using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{
    public class CountDownEventDemo
    {
        //We dont have SignalAndWait as in case of Barrier.
        //Signal and Wait are separate methods

        private static int taskCount = 5;
        private static Random random= new Random();
        static CountdownEvent cde = new CountdownEvent(taskCount);
        public static void Demo()
        {
            for(int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"We are entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cde.Signal();
                    Console.WriteLine($"Exiting the task {Task.CurrentId}");
                });
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                cde.Wait();
                Console.WriteLine("Tasks completed");
            });

            finalTask.Wait();
        }
    }
}
