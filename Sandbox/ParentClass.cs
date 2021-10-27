using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Sandbox
{
    class ParentClass
    {
        string _parentField;
        public void PrintField()
        {
            Console.WriteLine($"Parent field value from Parent class is {_parentField}");
                
        }
        public ParentClass(string parent)
        {
            _parentField = parent;
        }
        public ChildClass ReturnThis()
        {
            return (ChildClass)this;
        }
    }
}
