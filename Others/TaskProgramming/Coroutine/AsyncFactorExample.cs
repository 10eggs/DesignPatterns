using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.Coroutine
{
    public class AsyncFactorExample
    {
        //We cannot wait in constructor
        //public AsyncFactorExample()
        //{
        //    //await Task.Delay() <--- Forbidden
        //}

        private AsyncFactorExample()
        {

        }

        public static Task<AsyncFactorExample> CreateAsync()
        {
            var result = new AsyncFactorExample();
            return result.InitAsync();
        }
        public async Task<AsyncFactorExample> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }
        public static async Task Demo()
        {
            //    not the best approach
            //    var needToBeInitialize = new AsyncFactorExample();
            //    await needToBeInitialize.InitAsync();

            AsyncFactorExample x = await AsyncFactorExample.CreateAsync();
        }
    }
}
