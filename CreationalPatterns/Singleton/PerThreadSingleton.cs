using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Singleton
{
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
            = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());
        public static PerThreadSingleton Instance => threadInstance.Value;

        public int Id;
        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        //public static void Main(string [] args)
        //{
        //    var t1 = Task.Factory.StartNew(() =>
        //    {
        //        Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
        //    });
        //    var t2 = Task.Factory.StartNew(() =>
        //    {
        //        Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
        //        Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
        //    });

        //    Console.ReadLine();
        //}
    }
}
