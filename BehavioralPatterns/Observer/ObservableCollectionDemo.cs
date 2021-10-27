using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    public class ObservableCollectionDemo
    {

        //public static void Main(string[] args)
        //{
        //    var market = new Market();
        //    market.Prices.ListChanged += (sender, eventArgs) =>
        //    {
        //        if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
        //        {
        //            float price = ((BindingList<float>)sender)[eventArgs.NewIndex];
        //            Console.WriteLine($"Binding list get a price of {price}");
        //        }
        //    };
        //}
    }

    public class Market
    {
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrices(float price)
        {
            Prices.Add(price);
        }
    }
}
