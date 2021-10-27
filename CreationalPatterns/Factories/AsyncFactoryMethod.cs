using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns.Factories
{
    //Async invocation can be done inside methods, but not inside constructors.
    //It's a bit of problem cause sometimes you need to have asyc initialization
    //Most direct way to solve this problem is initialize constructor from async factory method
    class AsyncFactoryMethod
    {
        //public static async Task Main(string[] args)
        //{
        //   Foo x = await Foo.CreateAsync();
        //}
    }

    public class Foo
    {
        private Foo()
        {
            //It cannot be performed. Await only in async methods
            //await Task.Delay(1000);
        }

        //Private initialization
        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }
}
