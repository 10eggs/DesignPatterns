using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    //IObserver<type> object which is allowed to receive events of type
    public class ObservableDemo:IObserver<Event>
    {
        public ObservableDemo()
        {
            var patient = new Patient();
            //IDisposable sub = patient.Subscribe(this);

            patient.OfType<FallsIllEvent>()
                .Subscribe(args =>
                Console.WriteLine($"Doctor is required at {args.Address}"));

            patient.FallIll();

        }
        //public static void Main(string [] args)
        //{
        //    new ObservableDemo();
        //}

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Event value)
        {
            if(value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Address}");
            }
        }
    } 

    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }




    public class Patient : IObservable<Event>
    {

        //This IDisposable represent subscription itself
        //When you dispose this object you'll break the connection
        //
        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

        public void FallIll()
        {
            foreach(var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent { Address = "123 London" }  );
            }
        }
        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        private class Subscription : IDisposable
        {
            //Which object is this subscription subscribing to
            //Who is doing subscribing
            private readonly Patient patient;
            public readonly IObserver<Event> Observer;

            //Mechanism to store observable which is patient and observer
            public Subscription(Patient patient, IObserver<Event> observer)
            {
                this.patient = patient;
                Observer = observer; 
            }
            public void Dispose()
            {
                patient.subscriptions.Remove(this);
            }
        }
    }
}
