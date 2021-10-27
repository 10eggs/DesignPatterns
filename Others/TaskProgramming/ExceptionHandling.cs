using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming
{
    public class ExceptionHandling
    {
        public static void Demo()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Cant do this") { Source = "t" };
            });

            var t1 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {

                Task.WaitAll(t, t1);
            }
            catch(AggregateException ae)
            {
                foreach(var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
                }
            }
        }

        public static void WrapperForCatchSingleException()
        {
            try
            {
                CatchSingleException();
            }
            catch(AggregateException e)
            {
                foreach(var ie in e.InnerExceptions)
                {
                    Console.WriteLine($"Handled outside the CatchSingleException method: {ie.GetType()}");
                }
            }
        }

        public static void CatchSingleException()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Cant do this") { Source = "t" };
            });

            var t1 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {

                Task.WaitAll(t, t1);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        //returning true means that we are handling this exception
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }
    }
}
