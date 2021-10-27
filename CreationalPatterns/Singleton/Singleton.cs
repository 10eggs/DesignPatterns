using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Singleton
{
    //Where we can use singleton:
    //1. One object for whole system - db repo, object factory
    //2. Constructor call is expensive
    //3. Want to prevent anyone reating additional copies
    //4. Need to take care of lazy instantiations and thread safety
    class Singleton
    {
        public static string GetProjectPath()
        {
            var getPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = getPath.Substring(0, getPath.LastIndexOf("bin")) + "Helpers\\Capitals.txt";
            var projectPath = new Uri(actualPath).LocalPath;
            return projectPath;
        }
        //public static void Main(string[] args)
        //{
        //    var city = "Warsaw";
        //    Console.WriteLine($"Capital name: {city}, population: {SingletonDatabase.Instance.GetPopulation(city)}");
        //    Console.ReadLine();

        //}
    }

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            //Hardcoded reference to the singleton, tightly coupled 
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase database;
        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database;
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            //Hardcoded reference to the singleton, tightly coupled 
            foreach (var name in names)
                result += database.GetPopulation(name);
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }
    public class SingletonDatabase : IDatabase
    {
        Dictionary<string, int> capitals;
        private SingletonDatabase()
        {
            Console.WriteLine("Initialize database");
            string startupPath = Singleton.GetProjectPath();
            Console.WriteLine(startupPath);



            Console.Write(startupPath);
            capitals = File.ReadAllLines($"{startupPath}")
                .Batch(2)
                .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        //Lazy initialization

        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;

    }

    public class OrdinaryDatabase : IDatabase
    {
        Dictionary<string, int> capitals;
        public OrdinaryDatabase()
        {
            string startupPath = Singleton.GetProjectPath();

            capitals = File.ReadAllLines($"{startupPath}")
                .Batch(2)
                .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

    }
}
