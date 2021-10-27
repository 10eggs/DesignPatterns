using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.State
{

    public enum Chest
    {
        Open,
        Closed,
        Locked
    }

    public enum Action
    {
        Open,
        Close
    }
    public class SwitchExpressionDemo
    {
        public static Chest Manipulate(Chest chest, Action action, bool haveKey) =>
            (chest, action, haveKey) switch
            {
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,
                _ => chest
            };
        //public static void Main(string[] args)
        //{
        //    var chest = Chest.Locked;
        //    Console.WriteLine($"Chest is {chest}");

        //    chest = Manipulate(chest, Action.Open, true);
        //    Console.WriteLine($"Chest is {chest}");

        //    chest = Manipulate(chest, Action.Close, false);
        //    Console.WriteLine($"Chest is {chest}");

        //    chest = Manipulate(chest, Action.Close, false);
        //    Console.WriteLine($"Chest is {chest}");
        //}

    }
}
