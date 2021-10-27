using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }
    public class Driver
    {
        public int Age { get; set; }
    }
    public class CarProxy : ICar
    {
        private Driver _driver;
        private Car _car;
        public CarProxy(Driver driver)
        {
            _driver = driver;
        }
        public void Drive()
        {
            if (!(_driver.Age < 18))
            {
                Console.WriteLine("Too young");
            }
            else
            {
                Console.WriteLine("Ready to go!");
            }
        }
    }
    class Index
    {
    }
}
