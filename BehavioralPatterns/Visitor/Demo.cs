using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.VisitorDemo
{
    //Typically a tool for structure traversal rather than anything else
    //A pattern where component (visitor) is allowed to traverse the entire inheritance hierarchy.
    //Implemented by propagating single visit() method throughout the entire hierarchy.


    //Dispatch - which function are we going to call???
    //Single dispatch - depends on name of request and type of receiver
    
    //Double dispatch - depends on name of request and type of two rececivers(type of visitor,
    //type of element being visited)

    //Adding additional functionality when the hierarchy are already set and you cannot get into members itself and modify it
    public class Demo
    {
        //public static void Main(string[] args)
        //{
        //    var e = new AdditionExpression(
        //        new DoubleExpression(1),
        //        new AdditionExpression(
        //            new DoubleExpression(2),
        //            new DoubleExpression(3)
        //            ));

        //    var sb = new StringBuilder();
        //    e.Print(sb);
        //    Console.WriteLine(sb);
        //}
    }

    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        private double value;

        public DoubleExpression(double value)
        {
            this.value = value;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append(value);
        }
    }

    public class AdditionExpression : Expression
    {
        private Expression left, right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }
        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            left.Print(sb);
            sb.Append("+");
            right.Print(sb);
            sb.Append(")");
        }
    }
}
