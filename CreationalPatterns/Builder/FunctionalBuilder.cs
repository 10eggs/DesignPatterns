using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//https://martinfowler.com/bliki/FluentInterface.html
namespace DesignPatterns.CreationalPatterns.Builder
{
    class FunctionalBuilder
    {
        //public static void Main(string[] args)
        //{
        //    var person = new GenericPersonBuilder()
        //        .Called("Tomek")
        //        .WorksAs("Engineer")
        //        .Build();
        //    Console.Write(person);

        }

    public class PersonFunc
    {
        public string Name, Position;
    }

    public abstract class GenericFunctionalBuilder<TSubject, TSelf>
    where TSelf: GenericFunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions
           = new List<Func<Person, Person>>();

        //Aggregating all functions from actions list, execute them on new Person object
        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        public TSelf Do(Action<Person> action) => AddAction(action);
        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return (TSelf)this;
        }
    }

    public class GenericPersonBuilder
        : GenericFunctionalBuilder<Person, GenericPersonBuilder>
    {
        public GenericPersonBuilder Called(string name)
            => Do(p => p.Name = name);

        public GenericPersonBuilder Position(string pos)
            => Do(p => p.Position = pos);
    }




    public static class PersonBuilderExtensions
    {
        public static GenericPersonBuilder WorksAs
            (this GenericPersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);

    }
}



/*** Old approach without generics
 * 
 *  public static class PersonBuilderExtensions
    {
        public static PersonBuilderFunc WorksAs
            (this PersonBuilderFunc builder, string position)
        => builder.Do(p => p.Position = position);

    }
    public sealed class PersonBuilderFunc
    {
        private readonly List<Func<Person, Person>> actions
            = new List<Func<Person, Person>>();

        //Aggregating all functions from actions list, execute them on new Person object
        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
        public PersonBuilderFunc Called(string name) => Do(p => p.Name = name);
        public PersonBuilderFunc Do(Action<Person> action) => AddAction(action);
        private PersonBuilderFunc AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return this;
        }
    }
 * 
 * 
 * 
 */
