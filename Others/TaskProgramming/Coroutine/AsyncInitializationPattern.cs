using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.Coroutine
{

    public interface IAsyncInit
    {
        Task InitTask { get; }

    }

    public class MyClass :IAsyncInit
    {
        public Task InitTask { get; }
        public MyClass()
        {
            InitTask = InitAsync();
        }

        private async Task InitAsync()
        {
            await Task.Delay(1000);

        }
    }


    public class MyOtherClass : IAsyncInit
    {
        private readonly MyClass myClass;

        public Task InitTask { get; }
        public MyOtherClass(MyClass myClass)
        {
            this.myClass = myClass;
            InitTask = InitAsync();
        }

        private async Task InitAsync()
        {
            if (myClass is IAsyncInit ai)
                await ai.InitTask;
            await Task.Delay(1000);
        }
    }


    public class AsyncInitializationPattern
    {
        private readonly MyClass myClass;

        public static async void Demo()
        {
            var mc = new MyClass();
            if(mc is IAsyncInit iai)
            {
                await mc.InitTask;
            }

            var mc2 = new MyClass();
            var moc = new MyOtherClass(mc2);
            await moc.InitTask;
        }
    }
}
