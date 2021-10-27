using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Iterator
{
    public class ArrayBackedProperties:IEnumerable<int>
    {
        private int[] stats = new int[3];

        private const int strength = 0;
        public int AttOne
        {
            get => stats[strength];
            set => stats[strength] = value;
        }
        public int AttTwo
        {
            get => stats[1];
            set => stats[1] = value;
        }
        public int AttThree
        {
            get => stats[2];
            set => stats[2] = value;
        }

        public int this[int index]
        {
            get { return stats[index]; }
            set { stats[index] = value; }
        }
        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
