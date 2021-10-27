using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    public class WeakEventPattern
    {
        //public static void Main(string[] args)
        //{
        //    var btn = new Button();
        //    var window = new Window(btn);
        //    var windowRef = new WeakReference(window);
        //    btn.Fire();

        //    //So the reference to the window will not be collected by GC as long as there will be a button which subscribe ButtonOnClicked method from window class
        //    window = null;

        //    FireGC();
        //    Console.WriteLine($"Is the window alive after GC? {windowRef.IsAlive}");
        //}

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC IS DONE");
        }
    }

    public class Button
    {
        public event EventHandler Clicked;
        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            //button.Clicked += ButtonOnClicked;
            //From windowsBase
            //WeakEventManager<Button, EventArgs>.AddHandler(button, "Clicked", ButtonOnClicked);
        }

        private void ButtonOnClicked(object sender,EventArgs eventArgs)
        {
            Console.WriteLine("Button clicked (window handler)");
        }

        ~Window()
        {
            Console.WriteLine("Finalize");
        }
    }
}
