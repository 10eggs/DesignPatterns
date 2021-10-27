using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DesignPatterns.StructuralPatterns.Flyweight.Index;

namespace DesignPatterns.StructuralPatterns.Flyweight
{
    //Motivation
    //Avoid redundancy
    //Space optimization
    //Try to avoid duplication
    //String interning - strings are immutable
    //built in .NET string interning
    class Index
    {
        public class User
        {
            private string _fullName;
            public User(string fullName)
            {
                _fullName = fullName;
            }
        }
    }
    [TestFixture]
    public class Demo
    { 
        [Test]
        public void TestUser()
        {
            var firstNames = Enumerable.Range(1, 100).Select(_ => RandomString());
            var lastName = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();
            foreach (var fn in firstNames)
                foreach (var ln in lastName)
                {
                    users.Add(new User($"{fn} {ln}"));
                }

            ForceGC();
            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        public string RandomString()
        {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next())).ToArray());
        }
    }
}
