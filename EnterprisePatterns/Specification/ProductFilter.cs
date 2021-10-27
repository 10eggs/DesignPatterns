using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.EnterprisePatterns.Specification
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }

    }
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;

            }
        }
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color)
                    yield return p;

            }
        }
    }
}
