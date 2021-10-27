using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Others.RecursiveGenerics
{
    public class RecursiveGenerics
    {
        public static void Demo()
        {
            var basePart = Part.Empty.Add(355);
            var wheel = basePart.Add(45);
            var rim = wheel.Add(50);

            var total = basePart.TotalWeight;

            Console.WriteLine($"Total weight: {total}");
        }
    }

    /// <summary>
    /// Generic base class for a tree structure
    /// </summary>
    /// <typeparam name="T">The node type of the tree</typeparam>
    public abstract class Tree<TSelf> where TSelf : Tree<TSelf>
    {
        /// <summary>
        /// Constructor sets the parent node and adds this node to the parent's child nodes
        /// </summary>
        /// <param name="parent">The parent node or null if a root</param>
        /// 

        protected Tree(TSelf parent)
        {
            this.Parent = parent;
            this.Children = new List<TSelf>();
            if (parent != null)
            {
                parent.Children.Add(this as TSelf);
            }
        }
        public TSelf Parent { get; private set; }
        public List<TSelf> Children { get; private set; }
        public bool IsRoot { get { return Parent == null; } }
        public bool IsLeaf { get { return Children.Count == 0; } }
        /// <summary>
        /// Returns the number of hops to the root object
        /// </summary>
        public int Level { get { return IsRoot ? 0 : Parent.Level + 1; } }
    }

    public class Part: Tree<Part>
    {
        public static readonly Part Empty = new Part(null) { Weight = 0 };
        public Part(Part parent) : base(parent) { }

        public Part Add(float weight)
        {
            return new Part(this) { Weight = weight };
        }
        public float Weight { get; set; }

        public float TotalWeight { get { return Weight + Children.Sum(part => part.TotalWeight); } }


    }
}
