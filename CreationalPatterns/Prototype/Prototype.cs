using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace DesignPatterns.CreationalPatterns.Prototype
{

    //An existing  (partial or fully constructed) design is a Prototype

    //Motivation behind using prototype
    //1. Complicated objects arent designed from scratch
    //2. We make a copy (clone) the prototype and customize it [deep copy support]
    //3. Make cloning convenient(eg via Factory?)


    class Prototype
    {         
        public static void Demo()
        {
            //Deep copy or shallow copy
            //Deep cloning - copy all internal fields recursively
            //Shallow cloning - if you have internal fields, it copy only references

            var person = new Person(new[] { "Ana" }, new Address("Kasprzaka", 14));

            //Reference to the same object, making changes on one object
            var person2 = person;  
            person2.Names = new[] { "Dana" };
            //Dana for both
            Console.WriteLine(person);
            Console.WriteLine(person2);

            //Using shallow copy
            var person3 = (Person) person.Clone();
            person3.Address.HouseNumber=15;
            Console.WriteLine(person);
            Console.WriteLine(person3);

            //Using deep copy
            var person4 = person.DeepCopyXml();
            person4.Address.HouseNumber=16;

            Console.WriteLine(person);
            Console.WriteLine(person4);
        }        
    }

    //IClonable allow us to create shallow copy. It's not what we are looking for when we talk about prototype pattern
    //Let's implement deep copy inside ExtensionMethods class
    //We are using serializing here. Different serializers have different requiremens
    //Binary formatter require [Serializable] tag, and XML need to have parameterless constructor
    public static class ExtensionMethods
    {

        //It require [Serializable] tag before all classes which will be serializable
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            //Object serialized
            formatter.Serialize(stream, self);


            //Start deserialized object
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        //Extension for T means we have extension for all types
        public static T DeepCopyXml<T>(this T self)
        {
            //Using allow us to dispose memory stream when all operations inside will finish
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);

            }
        }
    }
    [Serializable]
    public class Person : ICloneable
    {
        public Person()
        {

        }
        public string[] Names;
        public Address Address;

        public Person(string [] names, Address address)
        {
            this.Names = names;
            this.Address = address;
        }

        public object Clone()
        {
            return new Person(Names, Address);
        }

        public override string ToString()
        {
            return $"Names: {string.Join(" ",Names)}, address: {Address.StreetName} {Address.HouseNumber}";
        }
    }

    [Serializable]

    public class Address
    {
        public Address()
        {

        }
        public string StreetName;
        public int HouseNumber;
        public Address(string streetName, int houseNumber)
        {
            this.StreetName = streetName;
            this.HouseNumber = houseNumber;
        }
    }
}
