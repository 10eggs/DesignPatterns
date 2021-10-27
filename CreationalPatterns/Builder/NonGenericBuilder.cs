using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder
{
    class NonGenericBuilder
    {
    }
    public class Person
    {
        public string Name;

        public string Position;


        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

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

    public class PersonInfoBuilder: PersonBuilder 
    {

        //Return type for this has to not refeer to current object, but to the type u get through inheritance
        public PersonInfoBuilder Called(string name)
        {
            person.Name = name;
            return (PersonJobBuilder)this;
        }
    }

    public class PersonJobBuilder: PersonInfoBuilder
    {
        public PersonJobBuilder WorkAsA(string position)
        {
            person.Position = position;
            return this;
        }
    }
}
