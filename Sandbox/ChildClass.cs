using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Sandbox
{
    class ChildClass : ParentClass
    {
        private string _childField;
        public void PrintField()
        {
            Console.WriteLine($"Child field from Child class is {_childField}");
        }

        public ChildClass(string child):base(child)
        {
            _childField = child;
        }

        public void PrintFromChiled()
        {
            Console.Write("I am printed from child");
        }

    }
}
