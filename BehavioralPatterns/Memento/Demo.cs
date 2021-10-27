using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Memento
{
    public class Demo
    {

        //public static void Main(string[] args)
        //{
        //    var ba = new BankAccount(100);
        //    var m1 = ba.Deposit(50);
        //    var m2 = ba.Deposit(50);
        //    var m3 = ba.Deposit(150);

        //    Console.WriteLine($"After deposiT: {ba}");

        //    ba.Undo();
        //    Console.WriteLine($"After first undo: {ba}");

        //    ba.Undo();
        //    Console.WriteLine($"After second undo: {ba}");

        //    ba.Undo();
        //    //100
        //    Console.WriteLine($"After third undo: {ba}");

        //    ba.Restore(m2);
        //    //350
        //    Console.WriteLine($"After restore of m3: {ba}");

        //    ba.Undo();
        //    //100
        //    Console.WriteLine($"This should be 150, but it is exception/null: {ba}");


        //    ba.Undo();
        //    //100
        //    Console.WriteLine($"This should be 150, but it is exception/null: {ba}");
           


        //}
    }

    public class BankAccount
    {
        private int balance;
        private List<Memento> changes = new List<Memento>();
        private int current;
        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            ++current;
            return m;
        }

        public Memento Restore(Memento memento)
        {
            if (memento != null)
            {
                balance = memento.Balance;
                changes.Add(memento);
                current = changes.Count-1;
                return memento;
            }
            return null;
        }

        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public Memento Redo()
        {
            if(current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }
}
