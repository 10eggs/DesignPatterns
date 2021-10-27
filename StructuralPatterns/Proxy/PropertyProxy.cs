using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    public class PropertyProxy<T> where T : new()
    {
        private T value;
        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value)) return;
                Console.WriteLine($"Assigning value to {value}");
                this.value = Value;
            }
        }
        public PropertyProxy(): this(Activator.CreateInstance<T>())
        {

        }
        public PropertyProxy(T value)
        {
            this.value = value;
        }

        //Assign ProperProxy<T> to T
        public static implicit operator T(PropertyProxy<T> property)
        {
            return property.Value; // int n = p_int;
        }

        //Assign T to ProperProxy<T>
        public static implicit operator PropertyProxy<T>(T value)
        {
            return new PropertyProxy<T>(value); // Property<int> p = 123;
        }

        public bool Equals(PropertyProxy<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if(obj.GetType()!=this.GetType()) return false;
            return Equals((PropertyProxy<T>)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(PropertyProxy<T> left, PropertyProxy<T> right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(PropertyProxy<T> left, PropertyProxy<T> right)
        {
            return !Equals(left, right);
        }

    }

    public class Creature
    {
        //Previous approach where we stored Agility directly behind property. If we will try to assign new value to this,
        //We end up with implicit conversion from T, which create new object = that is not what we want to have

        //This line should be commented
        //public PropertyProxy<int> AgilityProperty = new PropertyProxy<int>();

        private PropertyProxy<int> agility = new PropertyProxy<int>();
        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var c = new Creature();

        //    //c.AgilityProperty = 10;
        //    c.Agility = 10;
        //    //FIRST APPROACH ***** c.set_Agility(10) <= nope, cant override assignmnet operator
        //    //Instead there is a implicit conversion from T
        //    //It works like following ######### c.Agility = new Property<int>(10);

        //    PropertyProxy<int> p = 123;
        //    Debug.WriteLine($"{ p.Value} is here! (This value was assigned to the proxy)");

        //    int pValueConverted = p;
        //    Debug.WriteLine($"{ pValueConverted} is here(int type)!");

        //}
    }
}
