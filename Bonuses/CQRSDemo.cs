using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Bonuses.CQRS
{
    //CQRS - COMMAND QUERY RESPONSIBILITY SEGREGATION
    //CQS - COMMAND QUERY SEGREGATION
    //EventSourcing - you can review and rollback, things should be serializable

    //COMMAND - do/change

    //Potential Issues
    //
    //public class CQRSDemo
    //{
    //    //public static void Main(string[] args)
    //    //{
    //    //    var eb = new EventBroker();
    //    //    var person = new Person(eb);
    //    //    eb.Command(new ChangeAgeCommand(person, 123));


    //    //    foreach (var ev in eb.AllEvents)
    //    //    {
    //    //        Console.WriteLine(ev);
    //    //    }

    //    //    int age;
    //    //    age = eb.Query<int>(new AgeQuery(person));
    //    //    Console.WriteLine($"Age from query: {age}");


    //    //    eb.UndoLast();

    //    //    foreach (var ev in eb.AllEvents)
    //    //    {
    //    //        Console.WriteLine(ev);
    //    //    }

    //    //}
    //}

        public class Person
    {
        private int age { get; set; }
        EventBroker broker;
        public Person(EventBroker broker)
        {
            this.broker = broker;
            broker.Commands += BrokerOnCommands;
            broker.Queries += BrokerOnQueries;
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var cac = command as ChangeAgeCommand;
            if(cac != null && cac.Target==this)
            {
                if (cac.Register) broker.AllEvents.Add(new AgeChangedEvent(this, age, cac.age));
                age = cac.age;
            }
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var ac = query as AgeQuery;
            if (ac != null && ac.Target == this)
                ac.Result = age;
        }
    }



    public class EventBroker
    {
        //1. All elements that happened
        public IList<Event> AllEvents = new List<Event>();
        //2. Commands
        public EventHandler<Command> Commands;
        //3. Query
        public EventHandler<Query> Queries;

        public void Command(Command c)
        {
            Commands?.Invoke(this, c);
        }
        
        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T) q.Result;
        }

        public void UndoLast()
        {
            var e = AllEvents.LastOrDefault();
            var ac = e as AgeChangedEvent;
            if(ac != null)
            {
                Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Register = false });
                AllEvents.Remove(e);
            }
        }
    }

    public class Command:EventArgs
    {
        public bool Register = true;
    }

    public class ChangeAgeCommand : Command
    {
        public Person Target;
        public int age;

        public ChangeAgeCommand(Person target, int age)
        {
            Target = target;
            this.age = age;
        }
    }
    public class Query
    {
        public object Result;
    }

    public class AgeQuery : Query
    {
        public Person Target;
        public AgeQuery(Person target)
        {
            Target = target;
        }
    }

    public class Event
    {
        //1. backtrack - i.e. undo
        //2. 
    }

    public class AgeChangedEvent : Event
    {
        public Person Target;
        public int OldValue, NewValue;
        public AgeChangedEvent(Person target,int oldvalue, int newvalue)
        {
            Target = target;
            OldValue = oldvalue;
            NewValue = newvalue;
        }

        public override string ToString()
        {
            return $"Age changed from {OldValue} to {NewValue}.";
        }
    }
}
