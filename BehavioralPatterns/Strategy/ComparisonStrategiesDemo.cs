using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Strategy
{
    public class ComparisonStrategiesDemo
    {
        //public static void Main(string [] args)
        //{
        //    var people = new List<Person>();
        //    people.Sort(); //default strategy
        //    //people.Sort((x, y) => x.Name.CompareTo(y.Name));
        //    people.Sort(Person.NameComparer);
        //}
    }
    class Person : IComparable<Person>,IComparable
    {
        public int Id;
        public string Name;
        public int Age;

        public Person(int id, string name, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Person other ? CompareTo(other) : throw new ArgumentException();
        }

        public int CompareTo([AllowNull] Person other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public static bool operator < (Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) > 0;
        }

        public static bool operator <= (Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >= (Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) >= 0;
        }

        private sealed class NameRelationalComparer : IComparer<Person>
        {
            public int Compare([AllowNull] Person x, [AllowNull] Person y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;

                return string.Compare(x.Name, y.Name,
                    StringComparison.Ordinal);
            }
        }

        public static IComparer<Person> NameComparer { get; }
            = new NameRelationalComparer();
    }
}
