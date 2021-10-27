using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Mediator
{
    public class EventBroker : IObservable<PlayerEvent>
    {
        Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();
        public IDisposable Subscribe(IObserver<PlayerEvent> observer)
        {
            return subscriptions.Subscribe(observer);
        }
        public void Publish(PlayerEvent pe)
        {
            subscriptions.OnNext(pe);
        }

    }

    public class Actor
    {
        protected EventBroker broker;
        public Actor(EventBroker broker)
        {
            this.broker = broker;
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalScored { get; set; } = 0;

        public bool InGame { get; set; } = true;
        public void Score()
        {
            GoalScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name , GoalsScored = GoalScored});
        }

        public void AssaultReferee()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
        }
        public FootballPlayer(EventBroker broker, string name):base(broker)
        {
            
            Name = name;
            broker.OfType<PlayerScoredEvent>()
                .Where(pe => !Name.Equals(pe.Name) && InGame)
                .Subscribe(
                ps => Console.WriteLine($"{name}: Nicely done, {ps.Name}." +
                $" Number of your goals is {ps.GoalsScored}.")
                );

            broker.OfType<PlayerSentOffEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(ps => 
                    Console.WriteLine($"{name}: see you in the lockers, {ps.Name}"));

            broker.OfType<PlayerSentOffEvent>()
            .Where(ps => ps.Name.Equals(name))
            .Subscribe(ps => 
                    this.InGame = false);
        }
    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker):base(broker)
        {
            broker.OfType<PlayerScoredEvent>()
                .Subscribe(pe =>
                {
                    if(pe.GoalsScored < 3)
                        Console.WriteLine($"Coach: Well done, {pe.Name}!");
                });

            broker.OfType<PlayerSentOffEvent>()
                .Subscribe(pe =>
                {
                    if (pe.Reason == "violence")
                        Console.WriteLine($"Coach: How could you, {pe.Name}?");
                });
        }

    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }

    public class PlayerScoredEvent: PlayerEvent
    {
        public int GoalsScored { get; set; }
    }

    public class PlayerSentOffEvent: PlayerEvent
    {
        public string Reason { get; set; }
    }
}
