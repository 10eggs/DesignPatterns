using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.DynamicVisitorDemo
{
    public class DynamicVisitorDemo
    {
        //public static void Main(string[] args)
        //{
        //    Expression e = new AdditionExpression(
        //        new DoubleExpression(1),
        //        new AdditionExpression(
        //            new DoubleExpression(2),
        //            new DoubleExpression(3)
        //            ));
        //    var ep = new ExpressionPrinter();
        //    var sb = new StringBuilder();

        //    ep.Print((dynamic)e, sb);
        //    Console.WriteLine(sb);

        //}
    }

    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            //double dispatch
            visitor.Visit(this);
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

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter
    {
        public void Print(AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");

        }

        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }
}
