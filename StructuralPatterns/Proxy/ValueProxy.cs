using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    //public struct Price
    //{
    //    private int value;
    //    public Price(int value)
    //    {
    //        this.value = value;  
    //    }
    //}

    //Value proxy is wrapper around primitive type which provides bunch of conversion operator to and from that value
    public struct Percent
    {
        private readonly float value;
        internal Percent(float value)
        {
            this.value = value;
        }
        public static float operator *(float f, Percent p)
        {
            return f * p.value;
        }

        public static Percent operator +(Percent a, Percent b)
        {
            return new Percent(a.value + b.value);
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }

        public bool Equals(Percent other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Percent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }

    public static class PerentageExtensions
    {

        public static Percent Percent(this int value)
        {
            return new Percent(value / 100.0f);
        }

        public static Percent Percent(this float value)
        {
            return new Percent(value / 100.0f);
        }


    }
    class ValueProxy
    { 
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(
        //        10f * 5.Percent()
        //        );
        //    Console.WriteLine(
        //        2.Percent() + 3.Percent());
        //}
    }
}
