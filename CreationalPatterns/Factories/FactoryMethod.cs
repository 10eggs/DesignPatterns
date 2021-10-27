using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Factories
{
    //It's good to create objects of class where we need two different constructors with same parameters for both of them
    class FactoryMethod
    {
        //public static void Main(string[] args)
        //{
        //    Point point = Point.Factory.NewCartesianPoint(10,10);
            //Console.WriteLine(Point.OriginField);
            //Console.WriteLine(Point.OriginProp);
            //Console.WriteLine(Point.OriginProp);
            //Console.WriteLine(Point.OriginProp);
            //Console.WriteLine(Point.OriginProp);
            //Console.WriteLine(Point.OriginProp);
            //Console.WriteLine(Point.OriginField);
            //Console.WriteLine(Point.OriginField);
            //Console.WriteLine(Point.OriginField);
            //Console.WriteLine(Point.OriginField);
        //}
    }

    public class Point
    {
        private double x, y;

        //Initialized once
        public static Point OriginField = new Point(0,0);
        
        //Initialized every time when called
        public static Point OriginProp => new Point(0,0);
        //factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta)); 
        }

        //private constructor
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine("I am initialized!");
        }

        public override string ToString()
        {
            return $"X value: {this.x}, Y value: {this.y}";
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }



}
