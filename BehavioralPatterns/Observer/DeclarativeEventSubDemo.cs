using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Observer
{
    /**
     * This example should be reviewed at the end
     */
    public class DeclarativeEventSubDemo
    {
        // 
        //public static void Main(string[] args)
        //{
        //    //Container builder allow you to configure depependencies
        //    var cb = new ContainerBuilder();
        //    var ass = Assembly.GetExecutingAssembly();

        //    cb.RegisterAssemblyTypes(ass)
        //        .AsClosedTypesOf(typeof(ISend<>))
        //        .SingleInstance();

        //    cb.RegisterAssemblyTypes(ass)
        //        .Where(t => t.GetInterfaces()
        //        .Any(i =>
        //        i.IsGenericType &&
        //        i.GetGenericTypeDefinition() == typeof(IHandle<>)
        //        ))
        //        .OnActivated(act =>
        //        {
        //            //IHandle<Foo>
        //            //Go to the DI and find every ISend<Foo>
        //            var instanceType = act.Instance.GetType();
        //            var interfaces = instanceType.GetInterfaces();
        //            foreach (var i in interfaces)
        //            {
        //                if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>))
        //                {
        //                    //IHandle<Foo>
        //                    var args0 = i.GetGenericArguments()[0];
        //                    //ISend<Foo> construct!

        //                    var senderType = typeof(ISend<>).MakeGenericType(args0);

        //                    // every single ISend<foo> in container
        //                    //Di can resolve IEnumerable<> ->every instance of IFoo

        //                    var allSenderTypes = typeof(IEnumerable<>)
        //                        .MakeGenericType(senderType);
        //                    //IEnumerable<ISend<Foo>>
        //                    var allServices = act.Context.Resolve(allSenderTypes);
        //                    foreach (var service in (IEnumerable)allServices)
        //                    {
        //                        var eventInfo = service.GetType().GetEvent("Sender");
        //                        var handleMethod = instanceType.GetMethod("Handle");
        //                        var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, null, handleMethod);
        //                        eventInfo.AddEventHandler(service, handler);
        //                    }
        //                }

        //            }
        //        }).SingleInstance()
        //        .AsSelf();


        //    var container = cb.Build();
        //    var button = container.Resolve<Button>();
        //    var logging = container.Resolve<Logging>();

        //    button.Fire(1);
        //    button.Fire(2);
            
            /**
             * Container doesnt track the created objects.
             * You can have a problem where after the container has been created you registered ISender,
             * and all of created IHandled compontents arent going to subscribe to that sender.
             * 
             * 
             * How to unsusbscribe?
             * 
             */

        //}

        //Some compontent in the system send components to the other compontents
        public interface IEvent
        {

        }

        //Compontent which generate event
        public interface ISend<TEvent> where TEvent:IEvent
        {
            event EventHandler<TEvent> Sender;

        }

        public interface IHandle<TEvent> where TEvent: IEvent
        {
            void Handle(object sender, TEvent args); 
        }

        public class ButtonPressedEvent : IEvent
        {
            public int NumberOfClicks;

        }

        public class Button : ISend<ButtonPressedEvent>
        {
            public event EventHandler<ButtonPressedEvent> Sender;

            public void Fire(int clicks)
            {
                Sender?.Invoke(this, new ButtonPressedEvent { NumberOfClicks = clicks });
            }
        }

        public class Logging : IHandle<ButtonPressedEvent>
        {
            public void Handle(object sender, ButtonPressedEvent args)
            {
                Console.WriteLine($"The button was clicked {args.NumberOfClicks} clicked");
            }
        }
    }
}
