using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Decorator
{
    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException($"Cycled detected! Type is already a {type.FullName}");
            return true;
        }
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

    }
    class DetectingDecoratorCycle
    {
        //public static void Main(string[] args)
        //{
        //    var circle = new Circle(2);
        //    Console.WriteLine(circle.AsString());


        //    var colored1 = new ColoredShape(circle, "red");
        //    var colored2 = new ColoredShape(colored1, "blue");

        //    //This doesnt make sense
        //    Console.WriteLine(colored2.AsString());


        //}
    }
}
