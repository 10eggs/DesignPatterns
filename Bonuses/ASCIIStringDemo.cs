using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Bonuses
{
    public class ASCIIStringDemo
    {
        //public static void Main(string[] args)
        //{

        //}
    }

    //represent the ascii string
    //ordinary .net are utf-16
    public class str :IEquatable<str>
    {
        //single letter takes single byte
        //

        [NotNull] protected readonly byte[] buffer;
        public str()
        {
            buffer = new byte[] { };
        }
        public str(string s)
        {
            buffer = Encoding.ASCII.GetBytes(s);
        }

        protected str(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(buffer);
        }

        public static implicit operator str(string s)
        {
            return new str(s);
        }

        public bool Equals(str other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            //Comparison is made element by element
            return ((IStructuralEquatable)buffer)
                .Equals(other.buffer,
                StructuralComparisons.StructuralEqualityComparer);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        //str+str
        public static str operator +(str first,str second)
        {
            var bytes = new byte[
                first.buffer.Length + second.buffer.Length];
            first.buffer.CopyTo(bytes, 0);
            second.buffer.CopyTo(bytes, first.buffer.Length);
            return new str(bytes);
        }

        public char this[int index]
        {
            get => (char)buffer[index];
            set => buffer[index] = (byte)value;
        }
    }
}
