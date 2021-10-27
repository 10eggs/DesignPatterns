using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns
{
    class MultInheritanceWithInterfaces
    {


        //public static void Main(string [] args)
        //{
        //    Dragon dragon = new Dragon();
        //    //If interface has a default implementation, youve not have an access to this member right on the type, like it is when we are calling implemented interface.
        //    //So you can't invoke default methods from interfaces implemented by dragon.
        //    //dragon.Fly();

        //    //First way
        //    ((IBird)dragon).Fly();

        //    //Second way
        //    if(dragon is ILizard liz)
        //    {
        //        liz.Crawl();
        //    }
        //}

        public static void Demo()
        {

        }
    }
    public interface ICreature
    {
        public int Age { get; set; }

    }

    public interface IBird : ICreature
    {
        //Default
        void Fly()
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am flying!");
            }
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I am crawling");
            }
        }
    }

    public class Dragon : IBird, ILizard
    {
        public int Age { get; set; }
    }
}
