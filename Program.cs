using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DesignPatterns.BehavioralPatterns;
using DesignPatterns.CreationalPatterns.Builder;
using DesignPatterns.CreationalPatterns.Builder.Examples;
using DesignPatterns.CreationalPatterns.Builder.Exercise;
using DesignPatterns.CreationalPatterns.Factories.Exercise;
using DesignPatterns.CreationalPatterns.Prototype;
using DesignPatterns.CreationalPatterns.Singleton.Exercise;
using DesignPatterns.EnterprisePatterns.Specification;
using DesignPatterns.Others;
using DesignPatterns.Others.RecursiveGenerics;
using DesignPatterns.Others.TaskProgramming;
using DesignPatterns.Others.TaskProgramming.Coroutine;
using DesignPatterns.Others.TaskProgramming.ParallelLinq;
using DesignPatterns.Others.TaskProgramming.ParallelLoops;
using DesignPatterns.Others.TaskProgramming.TaskCoordination;
using DesignPatterns.Sandbox;
using DesignPatterns.StructuralPatterns.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static DesignPatterns.CreationalPatterns.Builder.HtmlElement;

namespace DesignPatterns
{       
    class Demo
    {

        public static void Main(string[] args)
        {
            DynamicDecoratorComposition.Demo();
        }
    }   
}

