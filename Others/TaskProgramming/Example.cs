using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming
{
    public class Example
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (--i > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (--i > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"Task with current id ${Task.CurrentId} is processing object ${o}...");
            return o.ToString().Length;
        }

        public static void StartTask()
        {
            Task.Factory.StartNew(() => Write('.'));

            var t = new Task(() => Write('?'));
            var t2 = new Task(Write, "Hello");

     
            t.Start();
            t2.Start();
            Write('-');
            Console.WriteLine("Main program is done");

        }

        public static void CheckingTasks()
        {
            string text1 = "first", text2 = "another_text";
            var task1 = new Task<int>(Example.TextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew(Example.TextLength, text2);


            Console.WriteLine($"Length of '{text1}' is ${task1.Result}");
            Console.WriteLine($"Length of '{text2}' is ${task2.Result}");


            Console.WriteLine("Main program is done");

        }
    }
}
