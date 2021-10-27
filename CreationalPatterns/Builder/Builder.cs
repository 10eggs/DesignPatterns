using DesignPatterns.Sandbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Builder
{

    class Builder
    {
        //public static void Main(string[] args)
        //{
            //      Common problem with inherited builders. If we inherit from builder, and we are trying to implement fluent api you can see, that
            //      we cant call our chain method on parent element, cause it does not contain method definition from child builder
            //      Clearly the problem with the fluent interfaces is you are not allowed containg type as a return type  
            //      //To get correct results we need to change our return type from both builders (need to do something more sophisticated).
            //      //1. How can the derived class propagate the information about returned to the base class?
            //      //To solve this issue we need to use recursive generics 

            //    By this snippet you downgrading your JobBuilder to NameBuilder 
            //    var builder = new PersonJobBuilder();
            //    builder.Called("Tomek")
            //        .WorksAs("Firefighter");

            //var me = Person.New()
            //    .Called("Tomek").GetType();
            //Console.WriteLine($"Type is: {me}");

            ////This piece of code works, even if childClass invoke parent metod which returned type 'this' is casted on child class
            //var childClass = new ChildClass("title");
            //Console.WriteLine(childClass.ReturnThis());
        //}
    }
    
    public class Person
    {
        public string Name;

        public string Position;


        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

        //Expose external API
        public class Builder : PersonJobBuilder<Builder>
        {

        }


        public static Builder New() => new Builder();
    }
    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

        //Type argument SELF inherit from themself
        //class Foo : Bar<Foo>

    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
    {

        //Return type for this has to not refeer to current object, but to the type u get through inheritance
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }
}


