using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLID.LiskovSubstitution
{
    /**
     * We should be able to upcast to our base type, and operation should be still generally ok, so child class should still behave as a child, even if you set base type reference to it
     * We can use virtual keyword to achieve this effect, so in our base class we would have "virtual" keyword
     */
    class LiskovSub
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
         
    }
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}L {Width}, {nameof(Height)}: {Height}"; 
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
        public override int Height
        {
            set
            {
                base.Height = base.Width = value;
            }
        }
    }
}
