using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Command
{
    public class Demo
    {
        //public static void Main(string [] args)
        //{
        //    //var ba = new BankAccount();
        //    //var commands = new List<BankAccountCommand>
        //    //{
        //    //    new BankAccountCommand(ba,BankAccountCommand.Action.Deposit,100),
        //    //    new BankAccountCommand(ba,BankAccountCommand.Action.Withdraw, 50)
        //    //};

        //    //Console.WriteLine(ba);

        //    //foreach(var c in commands)
        //    //{
        //    //    c.Call();
        //    //}

        //    //Console.WriteLine(ba);

        //    ////
        //    //foreach(var c in Enumerable.Reverse(commands))
        //    //{
        //    //    c.Undo();
        //    //}

        //    //var ba1 = new BankAccount();
        //    //var deposit = new BankAccountCommand(ba1, BankAccountCommand.Action.Deposit, 1000);
        //    //var withdraw = new BankAccountCommand(ba1, BankAccountCommand.Action.Withdraw, 500);

        //    ////Array is a IEnumerable which is ok
        //    //var composite = new CompositeBankAccountCommand(new[] { deposit, withdraw });
        //    //composite.Call();
        //    //Console.WriteLine(ba1);

        //    //composite.Undo();
        //    //Console.WriteLine(ba1);

        //    //var from = new BankAccount();
        //    //from.Deposit(100);

        //    //var to = new BankAccount();

        //    //var mtc = new MoneyTransferCommand(from,to,1000);
        //    //mtc.Call();

        //    //Console.WriteLine(from);
        //    //Console.WriteLine(to);

        //    //mtc.Undo();
        //    //Console.WriteLine(from);
        //    //Console.WriteLine(to);
        //    //mtc.Undo();
        //    //Console.WriteLine(from);
        //    //Console.WriteLine(to);
        ////}
    }

    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw ${amount}, balance is now {balance}");
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return $"The bank balance is {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
        public bool Success { get; set; }
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        private Action action;
        private int amount;
        public bool Success { get; set; }

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }
        public enum Action
        {
            Deposit,Withdraw
        }
        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!Success) return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    public class CompositeBankAccountCommand
        : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {

        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
        {

        }
        public bool Success
        {
            get
            {
                return this.All(cmd => cmd.Success);
            }
            set
            {
                foreach (var cmd in this)
                {
                    cmd.Success = value;
                }
            }
        }

        public virtual void Call()
        {
            ForEach(cmd => cmd.Call());
        }

        public virtual void Undo()
        {
            //this cast is required because list has it's own reverse() method
            foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                if (cmd.Success) cmd.Undo();

            }
        }
    }

    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from,BankAccountCommand.Action.Withdraw,amount),
                new BankAccountCommand(to,BankAccountCommand.Action.Deposit,amount)
            });
        }

        public override void Call()
        {
            BankAccountCommand last = null;
            foreach(var cmd in this)
            {
                if(last==null || last.Success)
                {
                    cmd.Call();
                    last = cmd;

                }
                else
                {
                    cmd.Undo();
                    break;
                }

            }

        }
    }
}
