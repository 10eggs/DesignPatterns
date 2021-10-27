using ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    //Dynamic proxy is constructed in runtime
    //
    public class DynamicProxyForLogging
    {
        //    public static void Main (string[] args)
        //    {
        //        //var ba = new BankAccount();
        //        //Dynamic proxy is a log for bank account, then we are returning it as an interface
        //        //IBank account. It gives you an interface you want from dynamic object
        //        var ba = Log<BankAccount>.As<IBankAccount>();


        //        ba.Deposit(100);
        //        ba.Withdraw(50);

        //        Console.WriteLine(ba);

        //    }
    }

    //Key thing here is we are going to have factory method
    //which going to be very sligh, and force our dynamic object to
    //behave as if it implement particular interface (in our case
    //IBankAccount)
    public class Log<T> :DynamicObject
        where T: class, new()
    {
        private readonly T subject;
        private Dictionary<string, int> methodCallCount
            = new Dictionary<string, int>();
        public Log(T subject)
        {
            this.subject = subject;
        }

        //Log<T> need to give us required interface
        public static I As<I>() where I : class
        {
            if (!typeof(I).IsInterface)
                throw new ArgumentException("I must be an interface type!");
            return new Log<T>(new T()).ActLike<I>();
        }
        //What is out and what is ref
        public override bool TryInvokeMember(InvokeMemberBinder binder,
            object[] args, out object result)
        {
            try
            {
                Console.WriteLine($"Invoking {subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",",args)}]");
                if (methodCallCount.ContainsKey(binder.Name)) methodCallCount[binder.Name]++;
                else methodCallCount.Add(binder.Name, 1);

                result = subject.GetType().GetMethod(binder.Name).Invoke(subject, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var kv in methodCallCount)
                    sb.AppendLine($"{kv.Key} called {kv.Value} time(s)");
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return $"{Info}\n{subject}";
        }

    }

    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }

    public class BankAccount : IBankAccount
    {
        private int balance;
        private int overdraftlimit = -500;
        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if(balance - amount >= overdraftlimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw ${amount}, balance is now {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }
}
