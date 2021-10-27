using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ConcurrentCollections
{
    public class ConcurrentDictionary
    {
        private static ConcurrentDictionary<string, string> capitals =
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            //We don't have 'Add' method here.
            //Return boolean to notice if you managed to add key-pair value or not
            bool success = capitals.TryAdd("France", "Paris");

            //This is nullable, so it will be null when it will be executed from main thread
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success? "added" : "not added")} the element ");
        }
        public static void Demo()
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();
        }
    }
}
