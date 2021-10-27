using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.NullObject
{
    public class Demo
    {
        //public static void Main(string[] args)
        //{

        //}
    }

    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }
        public void Warn(string msg)
        {
            Console.WriteLine("WARNING!!!: " + msg);
        }
    } 
}
