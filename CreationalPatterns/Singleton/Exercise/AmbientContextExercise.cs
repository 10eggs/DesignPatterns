using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Singleton.Exercise
{
    public class AmbientContextExercise
    {
        public static void Demo()
        {
            using(new PriceContext("Euro"))
            {
                var fly = new ExpensePln(100);
                Console.WriteLine(fly);
                using(new PriceContext("Pound"))
                {
                    var dinner = new ExpensePln(1000);
                    Console.WriteLine(dinner);
                }
                var uber = new ExpensePln(50);
                Console.WriteLine(uber);
            }
        }
    }

    //["Euro"] = 4.52,
    //["Pound"] = 5.32,
    //["Franc"] = 4.20



    public class Currencies
    {
        public static ReadOnlyDictionary<string, double> Currency = new ReadOnlyDictionary<string, double>
            (new Dictionary<string, double>
            {
                ["Euro"] = 4.52,
                ["Pound"] = 5.32,
                ["Franc"] = 4.20
            });
    }

    public class ExpensePln
    {
        private double expense;
        private double ratio;
        private string currency;
        public ExpensePln(double expense)
        {
            this.expense = expense;
            ratio = Currencies.Currency[PriceContext.Current.Currency];

        }

        public override string ToString()
        {
            return $"This expense in PLN ({expense}) is equal to {expense / ratio} {PriceContext.Current.Currency}";
        }
    }

    public class PriceContext:IDisposable
    {
        private static Stack<PriceContext> stack =
            new Stack<PriceContext>();

        public string Currency { get; set; }

        public PriceContext(string currency)
        {
            Currency = currency;
            stack.Push(this);
        }

        public static PriceContext Current => stack.Peek();
        public void Dispose()
        {
            if (stack.Count > 1)
            {
                stack.Pop();
            }
        }
    }
}
