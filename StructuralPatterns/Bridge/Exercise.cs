using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Bridge
{
    class Exercise
    {
        //public static void Main(string [] args)
        //{
        //    var figure = new Triangle(new VectorRender());
        //    var figure2 = new Triangle(new RasterRender());
        //    var figure3 = new Square(new VectorRender());
        //    var figure4 = new Square(new RasterRender());
        //    Console.WriteLine(figure);
        //    Console.WriteLine(figure2);
        //    Console.WriteLine(figure3);
        //    Console.WriteLine(figure4);
        //    Console.ReadLine();
        //}
        public abstract class Shape
        {
            protected IRenderer renderer;
            public string Name { get; set; }

            public Shape()
            {

            }
            public Shape(IRenderer renderer)
            {
                this.renderer = renderer;
            }

            public override string ToString()
            {
                return $"Drawing {Name} as {renderer.WhatToRenderAs}";
            }
        }

        public class Triangle : Shape
        {
    
            public Triangle(IRenderer renderer):base(renderer) => Name = "Triangle";
       
            public Triangle() => Name = "Triangle";

        }

        public class Square : Shape
        {
            public Square(IRenderer renderer):base(renderer) => Name = "Square";
            public Square() => Name = "Square";
        }

        public class VectorRender : IRenderer
        {

            public string WhatToRenderAs => "lines";
        }

        public class RasterRender : IRenderer
        {
            public string WhatToRenderAs => "pixels";
        }

        public interface IRenderer
        {
            public string WhatToRenderAs { get; }
        }
    }
}
