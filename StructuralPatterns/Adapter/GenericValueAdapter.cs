using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Adapter
{
    //We want to implement vectors. Vectors can have many different types.
    //Values of vectors can have different types (int, double, float).
    //At the end we want to have something like Vector2f, Vector3i.

    //In the vector class we want to have a constructor which initialize "data"
    //with an array with specific length(depends on dimension type argument
    //We cannot put literals as a generic type which means our array cant have lenght of D.

    //You cannot have class like Vector2f :Vector<float,2>, cause 2 is literal.
    //It leads us to generic value adapter pattern 
    //We are adapting literal value to tye type in this solution.

    //Our IInteger interface allow us to get necessary values(IDK how exactly it works, but it returns type in runtime rather than compiling time
    //Class "Two" implement this interface and returning necessary value
    //We can say that we've adapted value to the class

    //Now our Vector class type paramter D is restricted to inherit from IInteger and need to have default constructor, "new()"
    //We can add extra functionality for our vector, lets say we will have property for x,y,z.
    //However, when we introduce two dimension vector we are not necessary to have Z property inside.

    //So far we are initialising vectors value by properties. Let's change value initialisation and initialise it through constructor.
    //Unfortunately, we need to propagate constructors, so apart from having necessary constructor in base, we need to create specific constructor in child 

    //Let's add another functionality - adding two vectors.
    //We can not perform "+" operator on generic types, that's why we cannot implement this functionality in our base class
    //In c++ it's called partial specialization. We need to expand inheritance hierarchy, and specialize Vector class to integer.
    //In our currecnt hierarchy Vector->Vector2i we will add middle class called VectorOfInt

    //Our vector2i will no longer inherit from Vector - we want to have this ability to perform calculations, that's why we need inherit from vectorOfInt
    //[FACTORY]Let's say we want to avoid constructors propagation, and create a factory method for our Vector class.
    //[FIRST_FACTORY] When you call this method from children class, you will end up with instance of class Vector<T,D>, which does not know about implementation of 
    //+ operator for example.
    //[SECOND_FACTORY] We've introduced new generic type responsible for propagation of return type for derived factories (TSelf). We need to propagate
    //type param down to the class hierarchu



    //It wont work. Let's say you will call this method from our Vector3f class. What you will realize, that instead having Vector3f class, you will
    //End up with new object of type Vector<float,Dimensions.Three>. This class will not have expected functionality, like adding two vectors etc.
    //You cannot edit Vector3f class as well.
    //We need to implement recursive generics

    /// <summary>
    /// Class describing Vector element.
    /// </summary>
    /// <typeparam name="T">Type of value </typeparam>
    /// <typeparam name="D">Dimensions </typeparam>
    public class Vector<TSelf,T, D>
        where D:IInteger, new()
        where TSelf: Vector<TSelf, T, D>, new()
        //where TSelf : Vector<T, D, TSelf>,new()

    {
        public T[] data;
        public Vector()
        {
            //We want to have array which allows us to pass number of elements 
            //data = new T[?]
            data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            //Size we are going to store
            var requiredSize = new D().Value;
            data = new T[requiredSize];

            var providedSize = values.Length;
            for(int i=0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                data[i] = values[i];
            }
        }

        //[FIRST_FACTORY]
        //1st approach

        //public static Vector<T,D> Create (params T[] values)
        //{
        //    return new Vector<T, D>(values);
        //}

        //[RECURSIVE_GENERICS_APPROACH]
        //Try to return the most derived type of object
        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSize = new D().Value;
            result.data = new T[requiredSize];

            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                result.data[i] = values[i];
            }

            return result;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public T X
        {
            get => data[0];
            set => data[0] = value;
        }
        public T Y
        {
            get => data[1];
            set => data[1] = value;
        }
        public T Z
        {
            get => data[2];
            set => data[2] = value;
        }
    }

    //Previous solution
    //public class Vector2i : Vector<int, Dimensions.Two>
    //{
    //    public Vector2i()
    //    {

    //    }
    //    public Vector2i(params int[] values):base(values)
    //    {

    //    }
    //}


    //MiddleClass
    public class VectorOfInt<TSelf,D> : Vector<TSelf,int, D>
        where TSelf : Vector<TSelf, int, D>, new()
        where D : IInteger, new()
    {
        public VectorOfInt()
        {

        }
        public VectorOfInt(params int[] values) : base(values)
        {

        }

        public static VectorOfInt<TSelf,D> operator +
            (VectorOfInt<TSelf,D> lhs, VectorOfInt<TSelf,D> rhs)
        {
            var result = new VectorOfInt<TSelf,D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }
            return result;
        }
    }

    public class VectorOfFloat<TSelf,D>:Vector<TSelf,float,D>
        where TSelf :Vector<TSelf,float, D>, new()
        where D : IInteger, new()
    {
        public static VectorOfFloat<TSelf, D> operator +
        (VectorOfFloat<TSelf, D> lhs, VectorOfFloat<TSelf, D> rhs)
        {
            var result = new VectorOfFloat<TSelf, D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }
            return result;
        }
    }


    public class Vector2i : VectorOfInt<Vector2i,Dimensions.Two>
    {
        public Vector2i()
        {

        }
        public Vector2i(params int[] values) : base(values)
        {

        }
    }
    
    public class Vector3f : VectorOfFloat<Vector3f,Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", data)}";
        }
    }

  


    public interface IInteger
    {
        int Value { get; }
    }
    
    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }

    }


    class GenericValueAdapter
    {
        //    public static void Main(string[] args)
        //    {
        //        var v = new Vector2i(1, 2);
        //        var vv = new Vector2i(3, 4);

        //        //We can perform our calculation by using middlewareclass
        //        var result = v + vv;

        //        //[FIRST_FACTORY]
        //        //This is not a Vector3f, it's a Vector<float,Dimension.Three> instead
        //        //var u = Vector3f.Create(3.5f, 2.2f, 1);
        //        //u = u + u;

        //        //SecondApproach
        //        //This still not satisfying us, cause we retrieving a VectorOfFloat rather than Vector3f.
        //        var u = Vector3f.Create(3.5f, 2.2f, 1);
        //        var resultsAgain = u + u;

        //    }
    }
}
