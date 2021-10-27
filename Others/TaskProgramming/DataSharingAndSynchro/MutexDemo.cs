using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.DataSharingAndSynchro
{
    public class MutexDemo
    {
        public static void Demo()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            Mutex mutex = new Mutex(); 

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        //Stuck here and wait for this critical section will be available
                        bool haveLock = mutex.WaitOne();
                        try 
                        { 
                            ba.DepositNoLock(100);
                        }
                        finally
                        {
                            if (haveLock)
                                mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.WithdrawNoLock(100);
                        }
                        catch
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));
            }

            Console.WriteLine($"Total balance is {ba.Balance}");
        }

        public static void DemoWithTransfer()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        //Stuck here and wait for this critical section will be available
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.DepositNoLock(1);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.DepositNoLock(1);
                        }
                        catch
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //Can be used interchangebaly
                        //bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2,1);
                        }
                        finally
                        {
                            if(haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }

                    }
                }));
            }
            //Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Total balance for ba is {ba.Balance}");
            Console.WriteLine($"Total balance for ba2 is {ba2.Balance}");
        }
    }
}
