using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPatterns.Factories.Exercise
{
    class NotificationFactory
    {

        private List<Tuple<string, INotificationFactory>> factories
            = new List<Tuple<string, INotificationFactory>>();

        public NotificationFactory()
        {
            foreach (var t in typeof(NotificationFactory).Assembly.GetTypes())
            {
                if (typeof(INotificationFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), (INotificationFactory)Activator.CreateInstance(t)));
                }
            }
        }
        public static void Demo()
        {
            var notificationFactory = new NotificationFactory();
            Console.WriteLine("Available factories: ");
            foreach (var i in notificationFactory.factories)
            {
                Console.WriteLine($"{i.Item1}");

            }

            while (true)
            {
                string input;
                Console.WriteLine("Please specify notification");
                if ((input = Console.ReadLine()) != null && int.TryParse(input,out int i))
                {
                    var notification = notificationFactory.factories[i];
                    notification.Item2.FireNotification().Notify();

                }
            }
        }


        public interface INotification
        {
            public void Notify();
        }

        public class OperationSuccessNotification : INotification
        {
            public void Notify()
            {
                Console.WriteLine("Success!");
            }
        }

        public class OperationWarningNotification : INotification
        {
            public void Notify()
            {
                Console.WriteLine("This operation can cause an issue");
            }
        }

        public class OperationRejectedNotification : INotification
        {
            public void Notify()
            {
                Console.WriteLine("Operation rejected");
            }
        }

        public class PasswordExpireNotification : INotification
        {
            public void Notify()
            {
                Console.WriteLine("You need to change your password");
            }
        }

        public interface INotificationFactory
        {
            INotification FireNotification();
        }

        public class UserNotificationFactory : INotificationFactory
        {
            public INotification FireNotification()
            {
                Console.WriteLine("User notification: ");
                return new OperationSuccessNotification();
            }
        }

        public class AdminNotificationFactory : INotificationFactory
        {
            public INotification FireNotification()
            {
                Console.WriteLine("Admin notification");
                return new PasswordExpireNotification();
            }
        }


    }
}
