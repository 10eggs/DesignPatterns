using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{
    public class ChildTask
    {
        public static void Demo()
        {
            var parent = new Task(() =>
            {
                //detached - no difference between this task being here or somewhere else - it really doesnt matter for the scheduler
                var child = new Task(() =>
                {
                    Console.WriteLine("child start");
                    Thread.Sleep(3000);
                    Console.WriteLine("child finish");
                    throw new Exception();
                },TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray! Task {t.Id} state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops, task {t.Id} state is {t.Status}");
                },TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}
