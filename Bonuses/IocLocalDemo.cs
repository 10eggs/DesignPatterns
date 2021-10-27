using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Bonuses
{
    public class IocLocalDemo
    {
        //public static void Main(string[] args)
        //{
        //    var list = new List<int>();
        //    //Below list control functionality
        //    //list.Add(24);

        //    //Below control has been inverted to the number itself
        //    24.AddTo(list);

        //    Console.WriteLine(list.First());

        //    var person = new Person();
        //    person.Names.Add("Thomas");

        //    Console.WriteLine(person.HasName());

        //}
    }

    public class MyClass
    {
        public void AddingNumbers()
        {
            var list = new List<int>();
            list.Add(24);
        }

        public void ProcessCommand(string opcode)
        {
            //if (opcode == "AND" || opcode == "OR" || opcode == "XOR") { };
            //if (new[] { "AND", "OR", "XOR" }.Contains(opcode)) { };
            //if ("AND OR XOR".Split(" ").Contains(opcode)) { };
            //if(opcode.IsOneOf(new [] { "AND", "OR", "XOR" })) { };
        }

        public void Process(Person person)
        {
            //if(person.Names.Count == 0) { }
            //if (!person.Names.Any()) { }
            if (person.HasNo(p => p.Names)) { }
            if (person.HasSome(p => p.Names).And.HasNo(p => p.Children)) ;
        }
    }

    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();
    }

    public static class ExtensionMethods
    {
        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;
            public enum Operation
            {
                None,
                And,
                Or
            }

            internal Operation PendingOp;

            internal BoolMarker(bool result,T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }

            internal BoolMarker(bool result, T self) : this(result, self, Operation.None)
            {

            }

            public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }
        public static T AddTo<T>(this T self, params ICollection<T>[] colls)
        {
            foreach (var coll in colls)
            {
                coll.Add(self);
            };
            return self;
        }


        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static bool HasName(this Person self)
        {
            return self.Names.Count != 0;
        }

        public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>>props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(),self);
        }

        public static BoolMarker<T> HasNo<T,U>(this BoolMarker<T> marker, Func<T,IEnumerable<U>> props)
        {
            if (marker.PendingOp == BoolMarker<T>.Operation.And && !marker.Result)
                return marker;
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }

        public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(),self);
        }
    }
}
