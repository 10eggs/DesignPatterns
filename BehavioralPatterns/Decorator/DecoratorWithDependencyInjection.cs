using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Decorator
{
    class DecoratorWithDependencyInjection
    {
        //public static void Main(string[] args)
        //{
        //    var b = new ContainerBuilder();
        //    //b.RegisterType<ReportingService>().As<IReportingService>();
        //    b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
        //    b.RegisterDecorator<IReportingService>((context, service) =>
        //    new ReportingServiceWithLogging(service), "reporting");

        //    using (var c = b.Build())
        //    {
        //        var r = c.Resolve<IReportingService>();
        //        r.Report();
        //    }
        //}
    }

    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your report");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService decorated;

        public ReportingServiceWithLogging(IReportingService decorated)
        {
            this.decorated = decorated;
        }
        public void Report()
        {
            Console.WriteLine("Commencing report");
            decorated.Report();
            Console.WriteLine("Closing reporter");
        }
    }

}
