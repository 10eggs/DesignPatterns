using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.DataSharingAndSynchro
{
    public class ReaderWriterLock
    {
        //It allow to use recursion
        //public static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        public static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
        public static Random random = new Random();

        public static void Demo()
        {
            int x = 0;

            var tasks = new List<Task>();
            for(int i =0; i<10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //PAIR part I
                    padlock.EnterReadLock();
                    //padlock.EnterUpgradeableReadLock();

                    //if (i % 2 == 0)
                    //{
                    //    //By using UpgradebleLock we are allow to upgrade our readlock to writelock
                    //    padlock.EnterWriteLock();
                    //    x = 123;
                    //    padlock.ExitWriteLock();
                    //}
                    Console.WriteLine($"Entered readlock. Value of x = {x}");
                    Thread.Sleep(5000);


                    //PAIR part II
                    padlock.ExitReadLock();
                    //padlock.ExitUpgradeableReadLock();

                    Console.WriteLine($"Exited read lock, x ={x}");
                }));
            }
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    //when true is returned that means whether expcetion has been handled or not
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Writelock aquired");
                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x ={x}");
                padlock.ExitWriteLock();
                Console.WriteLine($"Write lock released");

            }
        }
    }
}
