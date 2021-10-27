using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Others.TaskProgramming.TaskCoordination
{

    //To invoke:
    //Task.WaitAll(SemaphoreSlimExample.CreateCall().ToArray());
    public class SemaphoreSlimExample
    {
        public static HttpClient _client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public static SemaphoreSlim ss = new SemaphoreSlim(1);
        public static void Demo()
        {
            var semapahore = new SemaphoreSlim(3,10);

            for(int i = 0; i < 30; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    semapahore.Wait(); //ReleaseCount--
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }

            while (semapahore.CurrentCount <= 3)
            {
                Console.WriteLine($"Semaphore count is {semapahore.CurrentCount}");
                Console.ReadKey();
                semapahore.Release(4);
            }
        }

        public static IEnumerable<Task> CreateCall()
        {
            for(int i=0; i < 200; i++)
            {
                yield return CallGoogle();
            }
        }
        public static async Task CallGoogle()
        {
            //When we are calling getasync is not the moment when request is send by network card.
            //This is just method invocation
            //Timeout start from moment when we called the function
            try
            {
                await ss.WaitAsync();
                var response = await _client.GetAsync("https://google.com");
                ss.Release();
                Console.WriteLine(response.StatusCode);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
