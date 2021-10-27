using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns
{
    //MOTIVATION
    //Want to augment an object with additional functionality
    //Do not want to rewrite or alter existing code (OCP)
    //Want to keep new functionality separate (SRP)
    //Need to be able to interact with existing structures
    //Facilites the addition of behaviors to individual objects without inheriting from them

    //Decorate StringBuilder
    //Adapter we attempting to conform an interface thats string builder gives you to you want specific interface which has
    //Implicit conversion from string and += operator

    //class Decorator
    //{
        //public static void Main (string [] args)
        //{
        //    //var s = "hello ";
        //    //s += "world";
        //    //Console.WriteLine(s);

        //    //StringBuilderDecorator s = "hello";
        //    //s += "World";
        //    //Console.WriteLine(s);

        //    StringBuilderDecorator txt = "hello ";
        //    txt += "my";
        //    Console.WriteLine(txt);

        //}
    //}

    //MySolution
    class StringBuilderDecorator
    {
        private StringBuilder stringBuilder = new StringBuilder();

        public static implicit operator StringBuilderDecorator(string s)
        {
            var msb = new StringBuilderDecorator();
            msb.stringBuilder.Append(s);
            return msb;
        }
        //public StringBuilderDecorator(string txt)
        //{
        //    stringBuilder.Append(txt);
        //}
        public StringBuilderDecorator()
        {

        }
        public StringBuilderDecorator(StringBuilder sb)
        {
            stringBuilder = sb;
        }

        public static StringBuilderDecorator operator +
            (StringBuilderDecorator self, string txt)
        {
            self.stringBuilder.Append(txt);
            return self;

        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }


    //Course solution
    public class MyStringBuilder
    {
        private StringBuilder sb = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb,
            string s)
        {
            msb.sb.Append(s);
            return msb;
        }
    }

}
