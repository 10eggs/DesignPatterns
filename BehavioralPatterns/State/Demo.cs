using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.State
{
    public class Demo
    {
        
        //public static void Main(string[] args)
        //{
        //    var ls = new Switch();
        //    ls.On();
        //    ls.Off();
        //    ls.Off();

        //}
    }

    //public class Switch
    //{
    //    public State State = new OffState();
    //    public void On() { State.On(this); }
    //    public void Off() { }
    //}

    //public abstract class State
    //{
    //    public virtual void On(Switch sw)
    //    {
    //        Console.WriteLine("Light is already on");
    //    }
    //    public virtual void Off(Switch sw)
    //    {
    //        Console.WriteLine("Light is already on");
    //    }
    //}

    //public class OnState : State
    //{
    //    public OnState()
    //    {
    //        Console.WriteLine("Light turned on");
    //    }

    //    public override void Off(Switch sw)
    //    {
    //        Console.WriteLine("Turning light off");
    //        sw.State = new OffState();
    //    }
    //}

    //public class OffState : State
    //{
    //    public OffState()
    //    {
    //        Console.WriteLine("Light turned off");
    //    }

    //    public override void On(Switch sw)
    //    {
    //        Console.WriteLine("Turning light on");
    //        sw.State = new OnState();
    //    }
    //}
}
