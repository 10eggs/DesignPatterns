using Autofac;
using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

//At the beginning we have only one button here. We need two buttons for Open and save command.
//WE need to register adapter

//Provide meta data

namespace DesignPatterns.StructuralPatterns.Adapter
{
    class AdapterInDependencyInjection
    {
        public static void Demo()
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("name", "Save");
            b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("name", "Open");
            //b.RegisterType<Button>();
            //Adapted item is ICommand. Who is doing the adapting - button
            //Resolve ICommand, look over results, and for each result make a button and inject this button
            b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd, ""));
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["name"]));
            b.RegisterType<Editor>();
            
            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
                editor.PrintAll();
            }

        }
    }

    public class Editor
    {
        private IEnumerable<Button> buttons;
        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }
        
        public void PrintAll()
        {
            foreach (var b in buttons)
                b.PrintMe();
        }
        
        public void ClickAll()
        {
            foreach (var b in buttons)
                b.Click();
        }
    } 
    public interface ICommand
    {
        void Execute();
    }
    class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving a file");
        }
    }

    class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening a file");
        }
    }

    public class Button
    {
        private string sillyName;
        private ICommand command;
        public Button(ICommand command, string name)
        {
            this.command = command;
            this.sillyName = name;
        }
        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {sillyName}");
        }
    }
}
