using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Singleton
{
    class AmbientContext
    {
        //public static void Main(string[] args)
        //{
        //    var house = new Building();
        //    //gnd 3000
        //    using (new BuildingContext(3000))
        //    {
        //        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        //        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

        //        using (new BuildingContext(3500))
        //        {
        //            //1st 3500
        //            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        //            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
        //        }
        //        //gnd 3000
        //        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        //        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
        //    }
        //}
    }

    public sealed class BuildingContext :IDisposable
    {
        public int WallHeight;
        private static Stack<BuildingContext> stack =
            new Stack<BuildingContext>();
        //Static constructor
        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }
        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public static BuildingContext Current => stack.Peek();
        public void Dispose()
        {
            if (stack.Count > 1)
            {
                //Remove last elem
                stack.Pop();
            }
        }
    }

    public class Building
    {
        public List<Wall> Walls = new List<Wall>();

    }
    public class Wall
    {
        public Point Start, End;
        public int Height;
        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight;
        }
    }
    public class Point
    {
        private int x,y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
