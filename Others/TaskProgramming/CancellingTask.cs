using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming
{
    public class CancellingTask
    {
        public static void CheckCancellation()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;


            token.Register(() =>
            {
                //It will run when task will be cancelled
                Console.WriteLine("Cancellation has been requested");
            });


            //Task has to be aware of that it is cancellable and it has to perform certain action to cancel iteself 
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //It can be done in this way, or just by using ThrowIfCancellationRequested()
                    //if (token.IsCancellationRequested)
                    //{
                    //    //break;

                    //    //It is recommended to use exception
                    //    //throw new OperationCanceledException();

                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{ i++}\t");
                    //}

                    //Canonical way to cancel task
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");

                }
            },token);

            t.Start();


            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been released");
            });


            Console.ReadKey();
            cts.Cancel();
        }

        public static void MultipleTokens()
        {
            var planned = new CancellationTokenSource();
            var preventive = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            planned.Token.Register(() =>
            {
                Console.WriteLine("This token has been cancelled");
            });

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventive.Token, emergency.Token);

            paranoid.Token.Register(() =>
            {
                Console.WriteLine("Composite token has been cancelled");
            });



            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(1000);
                }
            },paranoid.Token);

            Console.ReadKey();
            Console.WriteLine("Main task finished");

            planned.Cancel();
        }


    }
}
