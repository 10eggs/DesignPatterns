using DesignPatterns.EnterprisePatterns.Specification;
using System;
using System.Collections.Generic;
using System.Text;


//Open for extension, close for modification. It should be possible to add new filters rather than editing existing ones.
namespace DesignPatterns.SOLID
{
    class FiltersBeforeRefactoring
    {
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
}
