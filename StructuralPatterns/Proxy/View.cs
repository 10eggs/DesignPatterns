using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    public class View
    {
        //public static void Main(string [] args)
        //{

        //}
    }
    //MVVM

    //Model
    //View = ui
    //ModelView
    public class Person
    {
        public string FirstName, LastName;
    }

    public class PersonViewClass
    {
        private readonly Person person;

        public PersonViewClass(Person person)
        {
            this.person = person;
        }

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if (person.FirstName == value) return;
                person.FirstName = value;
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                if (person.LastName == value) return;
                person.LastName = value;
            }
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (value == null)
                {
                    FirstName = LastName = null;
                    return;
                }

                var items = value.Split();
                if (items.Length > 0)
                    FirstName = items[0];
                if (items.Length>1)
                    LastName = items[1];
            }
        }
    }
}
