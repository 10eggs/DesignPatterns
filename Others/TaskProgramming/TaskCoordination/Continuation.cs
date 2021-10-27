using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{
    public class Continuation
    {
        public static void Demo()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            //Following action is taking previous task as an argument
            var task2 =task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into the cup");
            });

            task2.Wait();
        }

        public static void DemoTaskReturnResult()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 }, tasks =>
              {
                  Console.WriteLine("Tasks has been completed");
                  foreach(var t in tasks)
                  {
                      Console.WriteLine($" - {t.Result}");
                  }
                  Console.WriteLine("All tasks done");
              });

            task3.Wait();
        }
    }
}
