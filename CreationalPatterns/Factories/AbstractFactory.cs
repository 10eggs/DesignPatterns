using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Factories
{
    //In the abstract setting your are not returning specific type.
    //Instead we are returning abstract classes or interfaces
    //We want to have different factories for different classes

    public interface ICarFrame
    {
        void AssambleWithDrive();
    }

    public class MercedesFrame : ICarFrame
    {
        public void AssambleWithDrive()
        {
            Console.WriteLine("This frame need to be assamble with AMG V8 engine");
        }
    }

    internal class BMWFrame : ICarFrame
    {
        public void AssambleWithDrive()
        {
            Console.WriteLine("This frame need to be assamble with BMW Engine");
        }
    }

    public interface ICarFrameFactory
    {
        ICarFrame Prepare(string material);
    }

    internal class MercedesFrameFactory : ICarFrameFactory
    {
        public ICarFrame Prepare(string material)
        {
            Console.WriteLine($"Mercedes frame prepared from {material}");
            return new MercedesFrame();
        }
    }

    internal class BMWFrameFactory : ICarFrameFactory
    {
        public ICarFrame Prepare(string material)
        {
            Console.WriteLine($"BMW frame prepared from {material}");
            return new BMWFrame();
        }
    }

    public class FramesFactory
    {
        //This enum here is violation of Open/Close principle - to add another frame type we will need to edit enum value inside FramesFactory class.
        //We are going to use reflection mechanism to make our factory more interactive. 
        //public enum AvailableFrame
        //{
        //    Mercedes, BMW
        //}
        //private Dictionary<AvailableFrame, ICarFrameFactory> factories =
        //    new Dictionary<AvailableFrame, ICarFrameFactory>();

        //public FramesFactory()
        //{
        //    foreach (AvailableFrame frame in Enum.GetValues(typeof(AvailableFrame)))
        //    {
        //        //Activator returns object, we need to cast it on AvailableFrames
        //        var frameFactory = (ICarFrameFactory)Activator.CreateInstance(
        //            Type.GetType("DesignPatterns.CreationalPatterns.Factories."
        //            + Enum.GetName(typeof(AvailableFrame), frame) + "FrameFactory")
        //            );
        //        factories.Add(frame, frameFactory);
        //    }
        //}

        //public ICarFrame PrepareFrame(AvailableFrame frame, string material)
        //{
        //    return factories[frame].Prepare(material);
        //}
        private List<Tuple<string, ICarFrameFactory>> factoriesList = new List<Tuple<string, ICarFrameFactory>>();

        public FramesFactory()
        {
            
            //Get all types from FrameFactory assembly
            foreach (var t in typeof(FramesFactory).Assembly.GetTypes())
            {
                //check if T implement ICarFrameFactory
                if(typeof(ICarFrameFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factoriesList.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty),(ICarFrameFactory)Activator.CreateInstance(t)));
                }
            }
        }

        public ICarFrame PrepareFrame()
        {
            //Need to check out keyword
            Console.WriteLine("Available frames: ");
            for(var index=0; index < factoriesList.Count; index++)
            {
                var tuple = factoriesList[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factoriesList.Count)
                {
                    Console.WriteLine("Specify material");
                    s = Console.ReadLine();
                    if(s != null)
                    {
                        return factoriesList[i].Item2.Prepare(s);
                    }
                }
            }
        }

    }

    public class AbstractFactory
    {
        //public static void Main(string [] args)
        //{
        //    var factory = new FramesFactory();
        //    var frame = factory.PrepareFrame();
        //    Console.WriteLine(frame);
        //}
    }


}
