using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{
    public class ResetEventExample
    {
        public static void Demo()
        {
            var evt = new ManualResetEventSlim();

            //AutoResetEvent is set on false.
            //Whe we reach are.Set() we are setting are for true, which allow us to run makeTea (we are invoking are.WaitOne())
            //However as this is AutoReset, this will be reset to false again, thats why second WaitOne() [marked as **SECOND**]
            //will hang our program forever

            //We can avoid to being stuck by using overloads:
            //as WaitOne() return boolean, we can invoke overloaded version with milSec arg, i.e. WaitOne(1000);
            var are = new AutoResetEvent(false);

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
                Thread.Sleep(3000);
                //evt.Set();
                are.Set();
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water");
                //evt.Wait();
                are.WaitOne();
                //Console.WriteLine("Here is your tea!");

                //**SECOND**
                //are.WaitOne

                var ok = are.WaitOne(1000);
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea");
                }
                else
                {
                    Console.WriteLine("No tea for you mejt");
                }
            });

            makeTea.Wait();
        }

    }
}
