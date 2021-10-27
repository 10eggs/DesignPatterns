using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Bonuses.MaybeMonadDemo
{
    public static class Maybe
    {
        //Check present or absent of something
        //TInput and TResult have to be nullable types (that's why cannot have value types for them)
        public static TResult With<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o,
            Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput:class
        {
            if (o == null) return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput,TResult>(this TInput o,
            Func<TInput,TResult>evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null) return failureValue;
            return evaluator(o);
        }

        public static TResult WithValue<TInput,TResult>(this TInput o,
            Func<TInput,TResult> evaluator)
            where TInput : struct
        {
            return evaluator(o);
        }
    }
    public class MaybeMonadDemo
    {
        public void MyMethod(Person person)
        {
            //string postcode = "Unknown";
            //if(person != null && person.Address !=null && person.Address.Postcode !=null)
            //    Console.WriteLine("Old approach");

            //new approach
            //postcode = person?.Address?.Postcode;

            //string postcode;
            //if (person != null)
            //{
            //    if(HasMedicalRecord(o) && person.Address != null)
            //    {
            //        CheckAddress(person.Address);
            //        if (person.Address.Postcode != null)
            //            postcode = person.Address.Postcode.ToString();
            //        else
            //            postcode = "UNKNOWN";
            //    }
            //}

            //Drill down
            string postcode = person.With(x => x.Address).With(x => x.Postcode);

            postcode = person
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.Postcode, "UNKNOWN");



        }

        private void CheckAddress(Address address)
        {
            throw new NotImplementedException();
        }

        private bool HasMedicalRecord(object o)
        {
            throw new NotImplementedException();
        }

        //public static void Main(string[] args)
        //{

        //}
    }

    public class Person
    {
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Postcode { get; set; }
    }
}
