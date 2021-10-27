using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Builder
{
    class FacetedBuilder
    {
        //public static void Main(string[] args)
        //{
        //   instead of using var we need to put explicit type, i.e.
        //
        //    PersonFacade person = new PersonBuilderFacade()
        //        .Works.At("Cool place")
        //        .AsA("Cool guy")
        //        .Earning(123)
        //        .Lives
        //        .WithPostcode("BS42UH");

        //    Console.WriteLine(person);
        //}
    }

    public class PersonFacade
    {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"Street address: {StreetAddress}, Postcode: {Postcode}, City: {City}," +
                $"Copany name: {CompanyName}, Position: {Position}";
        }

    }

    public class PersonBuilderFacade //facade
    {
        protected PersonFacade person = new PersonFacade();

        public PersonJobBuilderFacade Works => new PersonJobBuilderFacade(person);

        public PersonAddressBuilderFacade Lives => new PersonAddressBuilderFacade(person);

        //This is how we get another person without introducing any other piece of API 
        public static implicit operator PersonFacade(PersonBuilderFacade pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilderFacade : PersonBuilderFacade
    {
        public PersonJobBuilderFacade(PersonFacade person)
        {
            this.person = person;
        }
        public PersonJobBuilderFacade At(string companyName)
        {
            this.person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilderFacade AsA(string position)
        {
            this.person.Position = position;
            return this;
        }
        public PersonJobBuilderFacade Earning(int amount)
        {
            this.person.AnnualIncome = amount;
            return this;
        }

    }

    public class PersonAddressBuilderFacade : PersonBuilderFacade
    {
        public PersonAddressBuilderFacade(PersonFacade person)
        {
            this.person = person;
        }

        public PersonAddressBuilderFacade WithPostcode(string postcode)
        {
            this.person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilderFacade In(string city)
        {
            this.person.City = city;
            return this;
        }
    }

}
