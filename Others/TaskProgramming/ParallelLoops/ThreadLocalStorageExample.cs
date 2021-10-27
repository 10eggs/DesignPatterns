using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.ParallelLoops
{
    public class ThreadLocalStorageExample
        //x - counter
        //state(we dont use it)
        //tls is thread local storage, initialized by a thread local value
    {
        public static void Demo()
        {
            int sum = 0;
            Parallel.For(1, 11, () =>
            //Initial value of local storage - in our case is 0. Each of thread will start by increasing this value;
             0, 
             (x,state,tls)=>
             {
                 tls += x;
                 Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                 //As we kind of execute this iteration all those times we keep updating thread local variable
                 return tls;    
             },
             //thread local variable you get after end of every
             partialSum=>
             {
                 Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                 Interlocked.Add(ref sum, partialSum);
             });

            Console.WriteLine($"Sum of 1-11 ={sum}");
        }
    }
}
