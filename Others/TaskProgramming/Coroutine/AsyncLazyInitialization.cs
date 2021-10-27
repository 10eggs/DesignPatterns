using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.Coroutine
{
    public class AsyncLazyInitialization
    {
        public static void Demo()
        {

        }
    }

    public class Stuff
    {
        private static int value;

        private readonly Lazy<Task<int>> AutoIncVal =
            new Lazy<Task<int>>(async () =>
            {
                await Task.Delay(1000).ConfigureAwait(false);
                return value++;
            });

        private readonly Lazy<Task<int>> AutoIncVal2 =
            new Lazy<Task<int>>(() => Task.Run(async () =>
            {
                await Task.Delay(1000);
                return value++;
            }));

        public async void UseValue()
        {
            int value = await AutoIncVal.Value;
        }
    }
}
