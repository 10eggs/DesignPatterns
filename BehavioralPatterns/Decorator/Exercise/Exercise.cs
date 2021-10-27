using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Decorator.Exercise
{
    class Exercise
    {
        //public static void Main(string [] args)
        //{
        //    var d = new Dragon(new Lizard(), new Bird());
        //    d.Age = 0;
        //    Console.WriteLine(d.Crawl());
        //}
    }
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {

        private Lizard l;
        private Bird b;

        public Dragon(Lizard l, Bird b)
        {
            this.l = l;
            this.b = b;
        }
        public int Age
        {
            
            set
            {
                l.Age = value;
                b.Age = value;
            }
        }

        public string Fly()
        {
            return b.Fly();
        }

        public string Crawl()
        {
            return l.Crawl();
        }
    }
}
