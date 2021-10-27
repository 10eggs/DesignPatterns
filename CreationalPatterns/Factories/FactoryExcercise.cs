using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Factories
{
    class FactoryExcercise
    {
        //public static void Main(string [] args)
        //{
        //    var factory = new PersonFactory();
        //    var p1 = factory.CreatePerson("Tom");
        //    var p2 = factory.CreatePerson("Jon");
        //    var p3 = factory.CreatePerson("Adam");
        //    var p4 = factory.CreatePerson("Marta");

        //    Console.WriteLine(p1);
        //    Console.WriteLine(p2);
        //    Console.WriteLine(p3);
        //    Console.WriteLine(p4);
        //}
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }

    public class PersonFactory
    {
        private static int Id = 0;

        public Person CreatePerson(string name)
        {

            var person = new Person();
            person.Name = name;
            person.Id = Id++;
            return person;
        }

    }
}
