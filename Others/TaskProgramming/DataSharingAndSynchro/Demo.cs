using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.DataSharingAndSynchro
{
    public class Demo
    {

        //Thread.MemoryBarrier - sometimes CPU can actually reorder instructions which are written as you write them.

        public static void CheckBankAccount()
        {
            var tasks = new List<Task>();

            var ba = new BankAccount();

            for(int i =0; i<10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int i=0; i<1000; i++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }
        public static void CheckBankAccountWithInterlock()
        {
            var tasks = new List<Task>();

            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        ba.DepositWithInterlock(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        ba.WithdrawWithInterlock(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }


        //The idea that we are continuing waiting on something but we dont give up our position in schedule, we dont need to yield to other threads, instead we are wasting CPU cycles
        //SpinLock - 
        public static void CheckBankAccountWithSpinlock()
        {
            var tasks = new List<Task>();

            var ba = new BankAccount();

            //If you have two different operations trying to  take each others lock
            //We've got this mechanism to failing to take a lock

            SpinLock sl = new SpinLock();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        var lockTaken = false;
                        try
                        {
                            //trying to take a lock using sl.Enter
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
        }

        public static void LockRecursionHelper(int x)
        {
            SpinLock sl = new SpinLock(true);
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);

            }
            catch(LockRecursionException e)
            {
                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"We took a lock, x={x}");
                    LockRecursionHelper(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x={x}");
                }
            }
        }

    }

    public class BankAccount
    {
        
        public object padlock = new object();

        private int balance;
        public int Balance {
            get { return balance; }
            private set { balance = value; }
        }

        public void DepositNoLock(int amount)
        {
            balance += amount;
        }

        public void WithdrawNoLock(int amount)
        {
            balance -= amount;
        }
        public void Deposit(int amount)
        {
            //This operation is NOT atomic (two operations)
            //Op1. temp <- get_balance+amount
            //Op2. set_balance(temp);
            //Lock says 'ONLY ONE THREAD CAN ENTER HERE'

            //Lock is a shortcut of Monitor.Enter()/Exit()
            lock (padlock)
            {
            Balance += amount;

            }
        }
        public void Withdraw(int amount)
        {
            lock (padlock)
            {
            Balance -= amount;

            }
        }

        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }

        public void DepositWithInterlock(int amount)
        {
            Interlocked.Add(ref balance, amount);
        }

        public void WithdrawWithInterlock(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }
}
