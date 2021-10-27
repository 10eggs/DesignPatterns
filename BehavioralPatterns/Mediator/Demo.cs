using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Mediator
{
    /**
     * A component that facilitates communcation
     * between other components without them
     * necessarily being aware of each other or 
     * having direct(reference) access to each other
     * 
     */
    public class Demo
    {
        //public static void Main(string [] args)
        //{

            /**
             * Chat example
             *             
             *
            var room = new ChatRoom();

            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            jane.Say("oh, hey john!");

            var simon = new Person("Simon");
            room.Join(simon);

            simon.Say("HI everyone!");

            jane.PrivateMessage("Simon", "glad you could join us mate");
             */




            /**
             * Event Broker example
             
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => new FootballPlayer(
                    c.Resolve<EventBroker>(),
                    p.Named<string>("name")
                    ));


            using(var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var p1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var p2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                p1.Score();
                p1.Score();
                p1.Score();
                p1.AssaultReferee();
                p2.Score();

            }
            */



        //}
    }


    public class Person
    {
        public string Name;
        public ChatRoom Room;

        private List<string> chatLog = new List<string>();

        public Person(string name)
        {
            Name = name;
            
        }

        public void Say(string message)
        {
            Room.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);

        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatLog.Add(s);
            Console.WriteLine($"[{Name}]'s chat session] {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new List<Person>();
        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            Broadcast("room", joinMsg);

            p.Room = this;
            people.Add(p);
        }

        //This is where chat room mediating message
        public void Broadcast(string source, string message)
        {
            foreach(var p in people)
            {
                if(p.Name != source)
                {
                    p.Receive(source, message);
                }
            }
        }

        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }
}

