using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Proxy
{
    //SoA/AoS

    class GameCreature
    {
        public byte Age;
        public int X, Y;

    }
    class Creatuers
    {
        private readonly int size;
        private byte[] age;

        private int[] x, y;
        public Creatuers(int size)
        {
            this.size = size;
            this.age = new byte[size];
            this.x = new int[size];
            this.y = new int[size];
        }

        public struct CreatureProxy
        {
            private readonly Creatuers creatures;
            private readonly int index;
            public CreatureProxy(Creatuers creatures,int index)
            {
                this.creatures = creatures;
                this.index = index;
            }

            public ref byte Age => ref creatures.age[index];
            public ref int X => ref creatures.x[index];
            public ref int Y => ref creatures.y[index];

        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for(int pos=0; pos<size; ++pos)
            {
                yield return new CreatureProxy(this, pos);
            }
        }
    }

    class CompositeProxy
    {
        //public static void Main(string[] args)
        //{
            //AoS - array of structures
            //var creatures = new GameCreature[100];
            //foreach (var c in creatures)
            //{
            //    c.X++;
            //}

            //var creatures2 = new Creatuers(100);
            ////Structure of Arrays SoA
            //foreach (Creatuers.CreatureProxy c in creatures2)
            //{
            //    c.X++;
            //}
        //}
    }
}
