using DesignPatterns.BehavioralPatterns.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.BehavioralPatterns
{
    //When you can compose decorator?
    //Very common thing is to have two version of abstract class, Foo and Foo<T>:Foo
    
    //its all because of "is" keyword
    //if(x is Foo<> f) is invalid

    class DynamicDecoratorComposition
    {
        public static void Demo()
        {
            var square = new Square(1.23f);
            Console.WriteLine(square.AsString());
            var coloredSquare = new ColoredShape(square, "red");
            Console.WriteLine(coloredSquare.AsString());
            var transparedShape = new TransparentShape(coloredSquare, "60%");
            Console.WriteLine(transparedShape.AsString());
        }
    }

    public abstract class Shape
    {
        public abstract string AsString();
    }

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape shape;
        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if(shape is ShapeDecorator sd)
            {
                types.AddRange(sd.types);
            }
        }
    }
    //class with default policy

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
    where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new TCyclePolicy();
        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }


    public abstract class ShapeDecoratorWithPolicy<T> : ShapeDecorator<T, ThrowOnCyclePolicy>
    {
        public ShapeDecoratorWithPolicy(Shape shape):base(shape)
        {

        }
    }



    public class Circle:Shape
    {
        private float radius;
        public Circle(float radius)
        {
            this.radius = radius;
        }

        public override string AsString()
        {
            return $"Return circle with radius {radius}";
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class Square : Shape
    {
        private float side;
        public Square(float side)
        {
            this.side = side;
        }
        public override string AsString()
        {
            return $"Square with side {side}";
        }
    }

    public class ColoredShape : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private Shape shape;
        private string color;

        public ColoredShape(Shape shape, string color):base(shape)
        {
            this.shape = shape;
            this.color = color;
        }

        public override string AsString()
        {
            var sb = new StringBuilder($"{shape.AsString()}");
            //types initialised
            //type[0] -> current
            if (policy.ApplicationAllowed(types[0],types.Skip(1).ToList()))
            {
                sb.Append($" has the color {color}");
            }
            return sb.ToString();
        }
    }

    public class TransparentShape : Shape
    {
        private Shape shape;
        private string transparency;
        public TransparentShape(Shape shape, string transparency)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public override string AsString()
        {
            return $"{shape.AsString()} with transparency {this.transparency}";
        }
    }
}
