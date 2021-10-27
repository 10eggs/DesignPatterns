using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Adapter
{
    class AdapterExercise
    {
        //public static void Main()
        //{
        //    Square square = new Square();
        //    square.Side = 16;

        //    var sqtorc = new SquareToRectangleAdapter(square);
        //    Console.WriteLine(sqtorc.Area());
        //}
    }
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private Square square;
        public SquareToRectangleAdapter(Square square)
        {
            this.square = square;
            
        }

        public int Width => square.Side;

        public int Height => square.Side;
    }
}
