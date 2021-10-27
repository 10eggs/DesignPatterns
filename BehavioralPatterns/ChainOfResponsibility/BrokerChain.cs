using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.ChainOfResponsibility
{
    public class BrokerChain
    {

    }

    /**
     * Game is a MEDIATOR here.
     * Mediator pattern
     */
    public class Game
    {
        public event EventHandler<Query> Queries;
        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q); 
        }
    }
    public class Query
    {
        public enum Argument
        {
            Attack, Defence
        }
        public string CreatureName;
        public Argument WhatToQuery;
        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(paramName: nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public abstract class CreatureBCModifier : IDisposable
    {
        protected Game game;
        protected CreatureBC creature;
        public CreatureBCModifier(Game game, CreatureBC creature)
        {
            this.game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
            game.Queries += Handle;
        }

        //This handler is not intrusive - it doesn't have effect on sender itself, it affect
        //the query
        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            this.game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifierBC : CreatureBCModifier
    {
        public DoubleAttackModifierBC(Game game, CreatureBC creature):base(game,creature)
        {

        }

        protected override void Handle(object sender, Query q)
        {
            if(q.CreatureName == creature.Name
                && q.WhatToQuery == Query.Argument.Attack)
            {
                q.Value *= 2;
            }
        }
    }

    public class IncreaseDefenceModifierBC : CreatureBCModifier
    {
        public IncreaseDefenceModifierBC(Game game,CreatureBC creature) : base(game, creature)
        {

        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name
                    && q.WhatToQuery == Query.Argument.Defence)
            {
                q.Value *= 2;
            }
        }
    }
    public class CreatureBC
    {
        private Game game;
        public string Name;
        private int attack, defence;
        public CreatureBC(Game game, string name, int attack, int defence)
        {
            Name = name;
            this.game = game;
            this.attack = attack;
            this.defence = defence;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public int Defence
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defence, defence);
                game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defence)}: {Defence}";
        }
    }
}
