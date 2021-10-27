using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.SOLID.Dependency_Inversion
{
    public enum Relationship
    {
        Parent, Child, Sibiling
    }
    public class Person
    {
        public string Name;
        public DateTime DateOfBirth;
    }


    //So if we need to depend on low level module - we should depend of abstraction
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //Now even if implementation for storying data will change, we should receive same results as before refactorign
    public class Relationships : IRelationshipBrowser
    {
        //lower case for private field
        private List<(Person, Relationship, Person)> relations
        = new List<(Person, Relationship,Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Parent, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                x.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
            
        }

        //This property is a bad idea. We shouldnt expose our data in this way
        //public List<(Person, Relationship, Person)> Relations => relations;
    }
    //Research is high level module. We want to access low level module directly and perform research, which is bad idea
    public class Research
    {
        //public Research(Relationships relationship)
        //{
        //That's the rigid connection with low level class. It won't allow us to change our decision of how we store our relationship. We rely on
        //one implementation, and loss our flexibility in same time
        //To avoid this behavior we need to create an abstraction which allow low-level class to get access to higher level class, 

        //var relations = relationship.Relations;
        //foreach (var r in relations.Where(
        //    x => x.Item1.Name == "John" &&
        //    x.Item2 == Relationship.Parent))
        //{
        //    Console.WriteLine($"John has a child called {r.Item3.Name}");
        //}
        //}

        //Instead of rely on implementation of Relationship, we rely on IRelationshipBrowser interface
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                Console.WriteLine($"John has a child called {p.Name}");
        }
        //static void Main(string [] args)
        //{
        //    var parent = new Person { Name = "John" };
        //    var child1 = new Person { Name = "Chris" };
        //    var child2 = new Person { Name = "Mary" };

        //    var relationships = new Relationships();
        //    relationships.AddParentAndChild(parent, child1);
        //    relationships.AddParentAndChild(parent, child2);

        //    new Research(relationships);
        //}
    }
}

