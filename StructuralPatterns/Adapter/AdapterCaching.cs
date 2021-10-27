using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using MoreLinq;
using static System.Console;

namespace DesignPatterns.StructuralPatterns.AdapterWithCaching
{
    class AdapterCaching
    {
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
        {
          new VectorRectangle(1, 1, 10, 10),
          new VectorRectangle(3, 3, 6, 6)
        };

        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }

        // the interface we have
        public static void DrawPoint(Point p)
        {
            Write(".");
        }
        //public static void Main(string[] args)
        //{
        //    Draw();
        //}
    }
    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    public class Line
    {
        public Point Start;
        public Point End;

        public Line(Point start, Point end)
        {
            this.Start = start;
            this.End = end;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }
    }
    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }


    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int count = 0;
        //This is an example how we can add Cache mechanism to our adapter
        static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();
            if (cache.ContainsKey(hash))
                return;

            var points = new List<Point>();
            WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            //SelectMany(x=>x) makes one list from number of lists
            return cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
