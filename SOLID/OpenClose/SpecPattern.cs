using DesignPatterns.EnterprisePatterns.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLID.OpenClose
{
    class SpecPattern
    {
        //It dictates whether product some particular criteria you can think about specification as a kind of predicts which oparates on any type of T
        public interface ISpecification<T>
        {
            //We are specifying if particular item type T is satysfying some criteria
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }
        //Implement specification of product
        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;
            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;
            public SizeSpecification(Size size)
            {
                this.size = size;
            }
            public bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first, second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
                this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }


        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var i in items)
                {
                    if (spec.IsSatisfied(i))
                        yield return i;
                }
            }
        }
    }
}
