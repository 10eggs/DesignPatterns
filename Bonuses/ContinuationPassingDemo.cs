using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DesignPatterns.Bonuses
{
    public class ContinuationPassingDemo
    {
        //public static void Main(string[] args)
        //{
        //    var solver = new QuadraticEquationSolver();
        //    Tuple<Complex, Complex> solutions;
        //    var flag = solver.Start(1,10,16,out solutions);
        //    if(flag == WorkflowResult.Success)
        //    {
                
        //    }
        //}
    }

    public enum WorkflowResult
    {
        Success,Failure
    }
    public class QuadraticEquationSolver
    {
        //ax^2+bx+c == 0
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            //discriminent b^2-4ac
            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                result = null;
                return WorkflowResult.Failure;
            }
            else
            {
                return SolveSimple(a, b, c, disc, out result);
            }

        }
        private Tuple<Complex,Complex> SolveComplex(double a, double b,double c, double disc)
        {
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));
            return Tuple.Create(
                (-b - rootDisc) / (2 * a),
                (-b + rootDisc) / (2 * a)
                );
        }

        private WorkflowResult SolveSimple(double a, double b, double c, double disc, out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);
            result=Tuple.Create(
                new Complex((-b - rootDisc) / (2 * a), 0),
                new Complex((-b + rootDisc) / (2 * a), 0));

            return WorkflowResult.Success;

        }
    }
}
