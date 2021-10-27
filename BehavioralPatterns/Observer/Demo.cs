using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    public class Demo
    {
        //public static void Main(string[] args)
        //{
        //    var person = new Person();

        //    person.FallsIll += CallDoctor;

        //    person.CatchACold();
        //}

        private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
        {
            Console.WriteLine($"Doctor has been called to {eventArgs.Address}");
        }


    }

    public class Person
    {
        public void CatchACold()
        {
            //Need this question mark to prevent null reference
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "XYZ 14" });
        }
        public event EventHandler<FallsIllEventArgs> FallsIll;
    }

    public class FallsIllEventArgs : EventArgs
    {
        public string Address { get; set; }
    }
}
