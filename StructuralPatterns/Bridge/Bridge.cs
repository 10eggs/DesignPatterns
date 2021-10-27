using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Bridge
{
    //Connecting components together through abstraction
    //Bridge prevents a Cartesian product complexity explosion
    //A mechanism that decouples an interface(hierarchy) from an implementation (hierarchy)

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a cicle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    //Here is where the briging happens.Instead of specifing if shape is able to drawing itself, you dont put this limitation in place.
    //Instead we have to do is to build the bridge between the shape and whoever is drawing it
    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: "renderer");
        }

        public abstract void Draw();
        public abstract void Resize(float factor);

    }

    public class Circle : Shape
    {
        private float radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }
        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            this.radius *= factor;
        }
    }

    class Bridge
    {
        //public static void Main(string[] args)
        //{
        //    IRenderer renderer = new RasterRenderer();
        //    var circle = new Circle(renderer, 5);
        //    //circle.Draw();
        //    //circle.Resize(2);
        //    //circle.Draw();

        //    //Instead of passing arguments manually, lets run dependency injection container
        //    var cb = new ContainerBuilder();
        //    cb.RegisterType<VectorRenderer>().As<IRenderer>()
        //        .SingleInstance();
        //    //Circle is tricky - ctor needs two argument, we've got IRendered registered already, second arg is float arg
        //    //Float arg is something that you provide later on, not when you configure container , which is positional argument

        //    //The bridge pattern is really nothing more than a way of connecting the part of a system so we have a circle as a domain object 
        //    // and you connect your domain object to the different implementation of rendering mechanism. Instead of giving a circle method for drawing in raster or vector class
        //    //youare providing in by IRenderer
        //    cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
        //    using (var c = cb.Build())
        //    {
        //        var circleWithParameterInjection = c.Resolve<Circle>(
        //            new PositionalParameter(0, 5.0f)
        //            );
        //        circleWithParameterInjection.Draw();
        //        circleWithParameterInjection.Resize(2);
        //        circleWithParameterInjection.Draw();
        //    }

        //}
    }
}
