using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.ChainOfResponsibility
{
    public class Demo
    {
        //public static void Main()
        //{
            //var goblin = new Creature("Goblin", 2, 2);
            //Console.WriteLine(goblin);

            //var root = new CreatureModifier(goblin);

            //Console.WriteLine("Let's double the goblin's attack");
            //root.Add(new DoubleAttackModifier(goblin));

            //Console.WriteLine("Let's double the goblin's defence");
            //root.Add(new IncreaseDefenceModifier(goblin));

            //root.Handle();
            //Console.WriteLine(goblin);

        //    var game = new Game();
        //    var goblin = new CreatureBC(game, "Strong Goblin", 3, 3);
        //    Console.WriteLine(goblin);
        //    using (new DoubleAttackModifierBC(game, goblin))
        //    {
        //        Console.WriteLine($"Doubled attack: {goblin}");
        //        using (new IncreaseDefenceModifierBC(game, goblin))
        //        {
        //            Console.WriteLine($"Doubled defence: {goblin}");
        //        }
        //        Console.WriteLine($"No extra defence {goblin}");
        //    }

        //    Console.WriteLine($"No extra attack: {goblin}");

        //}
    }

    public class Creature
    {
        public Creature(string name, int attack, int defence)
        {
            Name = name;
            Attack = attack;
            Defence = defence;
        }
        public string Name;
        public int Attack, Defence;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defence)}: {Defence}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException();
        }

        public void Add(CreatureModifier creatureModifier)
        {
            if (next != null)
                next.Add(creatureModifier);
            else
                next = creatureModifier;
        }

        public virtual void Handle() => next?.Handle();

    }

    public class DoubleAttackModifier:CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {

        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();

        }
    }

    public class IncreaseDefenceModifier : CreatureModifier
    {
        public IncreaseDefenceModifier(Creature creature):base(creature)
        {

        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s defence");
            creature.Defence *= 2;
            base.Handle();
        }
    }

    public class NoBonusModifier : CreatureModifier
    {
        public NoBonusModifier(Creature creature):base(creature)
        {

        }

        public override void Handle()
        {
            //no action here
        }
    }

        
}
