using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.StructuralPatterns.Adapter.Exercise
{
    //    Say I want to mock out a framework class which doesn't implement an interface (and doesn't have virtual methods).
    //    With many mocking apis this is hard or impossible to do.
    //    What I will do, then, is define my own interface as a subset of the signature of the class I'm targeting.
    //    I implement a wrapper class that implements this interface and simply delegates the calls to the wrapped framework class.
    //    This wrapper class works as an adapter for the framework class.
    //    My classes use this adapter instead of the framework class, but get the framework class' behavior.
    public class FrameworkClassWrapper
    {
        public static void Demo()
        {

        }
    }

    public interface IFoo
    {
        void Bar();
    }

    public class FrameworkFoo
    {
        public void Bar() { }
    }
    public class FooWrapper : IFoo
    {
        private FrameworkFoo ff;
        public FooWrapper(FrameworkFoo ff)
        {
            this.ff = ff;
        }
        public void Bar()
        {
            this.ff.Bar();
        }
    }


    //    Consider also the case where you have a couple of different classes that have basically the same functionality,
    //    but different signatures and you want to be able to use them interchangeably.
    //    If you can't transform these (or don't want to for other reasons),
    //    you may want to write an adapter class that defines a common interface and translates between that
    //    interface's methods and the methods available on the target classes.


    public class TargetA
    {
        public void Start() { }
        public void End() { }
    }

    public class TargetB
    {
        public void Begin() { }
        public void Terminate() { }
    }

    public interface ITargetAdapter
    {
        public void Open();
        public void Close();
    }

    public class AdapterA : ITargetAdapter
    {
        private TargetA targetA;
        public AdapterA(TargetA a)
        {
            targetA = a;
        }
        public void Close()
        {
            targetA.End();
        }

        public void Open()
        {
            targetA.Start();
        }
    }
}
