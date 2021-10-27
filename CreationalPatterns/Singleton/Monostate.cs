using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Singleton
{
    class Monostate
    {
      //public static void Main(string [] arguments)
      //  {
      //      var person1 = new Person();
      //      var person2 = new Person();
      //      person1.Name = "Thomas Shelby";
      //      person2.Name = "Thomas Shelby";

      //      Console.WriteLine(person1.Name);
      //      Console.WriteLine(person2.Name);
      //      Console.ReadLine();
      //  }
    }

    public class Person
    {
        //static fields wrapped by properties
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

    }
}
