using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Visitor
{
    //The question is how to add print functionality to the class hierarchy
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;
    public class ReflectionBasedVisitor
    {
        //public static void Main(string[] ags)
        //{
        //    var e = new AdditionExpression(
        //        new DoubleExpression(1),
        //        new AdditionExpression(
        //            new DoubleExpression(2),
        //            new DoubleExpression(3)
        //            ));
        //    var sb = new StringBuilder();
        //    ExpressionPrinter.Print(e, sb);
        //    Console.WriteLine(sb);

        //}
    }
    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append(Value);
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left, Right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            Left.Print(sb);
            sb.Append("+");
            Right.Print(sb);
            sb.Append(")");
        }
    }

    public static class ExpressionPrinter
    {
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression)] = (e, sb) =>
            {
                var ae = (AdditionExpression)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };
        public static void Print(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb); 
        }
    }
}
