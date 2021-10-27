using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    public class PropertyDependenciesDemo
    {
        //public static void Main(string[] args)
        //{
        //    var p = new Man();
        //    p.PropertyChanged += (sender, eventArgs) =>
        //    {
        //        Console.WriteLine($"{eventArgs.PropertyName} changed");
        //    };

        //    p.Age = 15;
        //    p.Citizen = true;
        //}
    }

    public class Man:PropertyNotificationSupport
    {
        private int age;
        public int Age
        {
            get => age;
            set
            {
                //var oldCanvote = CanVote;
                if (value == age) return;
                age = value;
                OnPropertyChanged();

            }
        }
        private bool citizen;
        public bool Citizen {
            get => citizen;
            set
            {
                if (value == citizen) return;
                citizen = value;
                OnPropertyChanged();
            }

        }

        //We dont want to point manually any property dependencies in our support class, thats why we are going to use expression tree
        //public bool CanVote => Age > 16;

        private readonly Func<bool> canVote;
        public bool CanVote => canVote();

        public Man()
        {
            //Name of property, what expression is use to calculate
            canVote = property(nameof(CanVote), () => Age >= 16 && Citizen);
        }


    }

    public class PropertyNotificationSupport : INotifyPropertyChanged
    {
        //This will be the place where we are setting dependency graph. We will have name of property with assigned hashset of properties which this property is affected by.
        //CanVote -> Age, Citizen, Etc.
        private readonly Dictionary<string, HashSet<string>> affectedBy
            = new Dictionary<string, HashSet<string>>();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


            //Try to add circular dependency check
            foreach(var affected in affectedBy.Keys)
            {
                if (affectedBy[affected].Contains(propertyName))

                    //We havent verified if the property has infact been modified
                    //We are calling this dependency graph unconditionally, but we should run it conditionally
                    OnPropertyChanged(affected);
                    
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Lower case because it is protected?
        protected Func<T> property<T>(string name, Expression<Func<T>> expr)
        {
            Console.WriteLine($"Creating computed property for expression {expr}");
            var visitor = new MemberAccessVisitor(GetType());
            visitor.Visit(expr);

            if (visitor.PropertyNames.Any())
            {
                if (!affectedBy.ContainsKey(name))
                    affectedBy.Add(name, new HashSet<string>());
                 
                foreach(var propName in visitor.PropertyNames)
                {
                    if (propName == name)
                        affectedBy[name].Add(propName);
                }
            }
            return expr.Compile();

        }

        private class MemberAccessVisitor : ExpressionVisitor
        {
            private readonly Type declaringType;
            public readonly IList<string> PropertyNames = new List<string>();
            
            public MemberAccessVisitor(Type declaringType)
            {
                this.declaringType = declaringType;
            }

            public override Expression Visit(Expression expr)
            {
                //Our expression type is not a member access - it is lambda
                if (expr != null && expr.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = (MemberExpression)expr;
                    if (memberExpr.Member.DeclaringType == declaringType)
                        PropertyNames.Add(memberExpr.Member.Name);

                }
                return base.Visit(expr);
            }
        }
    }
}
