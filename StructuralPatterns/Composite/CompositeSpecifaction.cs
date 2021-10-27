using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Composite
{
    //https://dotnettutorials.net/lesson/composite-design-pattern/
    class CompositeSpecifaction
    {

    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, CompositeSpecification<T> spec);
    }


    public abstract class ISpecification<T>
    {
        //It is allowed to have operator definition only in classes
        //We are specifying if particular item type T is satysfying some criteria
        public abstract bool IsSatisfied(T t);

        public static ISpecification<T> operator &
            (ISpecification<T> first, ISpecification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        protected ISpecification<T>[] items;
        public CompositeSpecification(params ISpecification<T>[] items)
        {
            this.items = items;
        }
    }
    //Combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params ISpecification<T>[] items) : base(items)
        {

        }
        public override bool IsSatisfied(T t)
        {
            return items.All(i => i.IsSatisfied(t));

        }
    }

    //Implement specification of product
    //public class ColorSpecification : ISpecification<Product>
    //{
    //    private Color color;
    //    public ColorSpecification(Color color)
    //    {
    //        this.color = color;
    //    }
    //    public bool IsSatisfied(Product t)
    //    {
    //        return t.Color == color;
    //    }
    //}



}
