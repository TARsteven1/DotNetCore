using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest._3AsyncAwait
{
    /// <summary>
    /// 对Task进行序列化（即按一定顺序执行），使用async与await更加方便，否则
    /// 否则就要在continuation内部触发下一次循环的实现代码复杂且行多
    /// </summary>
    class MyAsyncAwait
    {
        static async Task Main(string[] srgs)
        {
            await DisplayPrimeCountsAsync();
        }

        async static Task DisplayPrimeCountsAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await GetPrimesCountAsync(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) +
                    " and " + ((i + 1) * 1000000 - 1));
                Console.WriteLine("Done!");
            }
        }

        private static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
}
