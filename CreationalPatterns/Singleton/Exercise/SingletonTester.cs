using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Singleton.Exercise
{
    class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func();
            var obj2 = new Object();
            return obj1.Equals(obj2) ? true : false;
        }
        //public static void Main (string [] args)
        //{
        //    Console.WriteLine(SingletonTester.IsSingleton(SingletonFactory.GetSingleton));
        //    Console.ReadLine();
        //}

    }

    public class Singleton
    {
        private static Singleton instance = new Singleton();

        private Singleton()
        {

        }
        public static Singleton GetSingleton()
        {
            return instance;
        }
    }

    public class SingletonFactory
    {
        private static Object Singleton = new Object();
        public static Object GetSingleton() {
            return Singleton;
        }
        private SingletonFactory()
        {

        }

    }


}
