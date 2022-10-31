using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest._4Await
{
    class MyAwait
    {
        void MyAwaiting()//其结构如下：
        {
            //var result = await expression; 
            //statement(s);
        }

        void SameMyAwaiting()//它的作用相当于： 
        {
            //var awaiter = expression.GetAwaiter();
            //awaiter.OnCompleted(() =>
            //{
            //    var result = awaiter.GetResult();
            //    statement(s);
            //});
        }

        /* async 修饰符 
         * · async 修饰符会让编译器把 await 当作关键字而不是标识符（ C#5 以前可能会使用 await 作为标识符） 
         * · async 修饰符只能应用于方法（包括 lambda 表达式）。 
         *   · 该方法可以返回 void 、 Task 、 Task < TResult > 
         * · async 修饰符对方法的签名或 Public 元数据没有影响（和 unsafe 一样），它只会影响方法内部。
         *   · 在接口内使用 async 是没有意义的使用 async 来重载非 async 的方法却是合法的（只要方法签名一致） 
         *   · 使用了 async 修饰符的方法就是“异步函数”。
         */

        /*异步方法如何执行 
         * · 遇到 await 表达式，执行（正常情况下）会返回调用者
         * · 就像 iterator 里面的 yield return 。
         * · 在返回前，运行时会附加一个 continuation 到 await 的 task
         * · 为保证 task 结束时，执行会跳回原方法，从停止的地方继续执行。
         * · 如果发生故障，那么异常会被重新抛出
         * · 如果一切正常，那么它的返回值就会赋给 await 表达式（例子如下）
         */
        static async Task Main(string[] srgs)
        {
            await DisplayPrimesCountAsync();
        }
        //此方法效果与下个方法一致
         void DisplayPrimescount()
        {
            var awaiter = GetPrimesCountAsync(2, 1000000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                int result = awaiter.GetResult();
                Console.WriteLine(result);
            });
        }


        static async Task DisplayPrimesCountAsync()
        {
            int result = await GetPrimesCountAsync(2, 1000000);
            Console.WriteLine(result);
        }

        private static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
        /*
         * 可以 await 什么？
         *  · 你 await 的表达式通常是一个 task 
         *  · 也可以满足下列条件的任意对象：
         *    · 有 Ge1AWaiter 方法，它返回一个 awaiter （实现了 INotifyComPletion . OnCompleted 接口）
         *    · 返回适当类型的 GetResult 方法
         *    · 一个 bool 类型的 IsCompleted 属性
         */


        //捕获本地状态(如下例子)
        //· await 表达式的最牛之处就是它几乎可以出现在任何地方。
        //· 特别的，在异步方法内， await 表达式可以替换任何表达式。
        //  · 除了 lock 表达式和 unsafe 上下文
        static async Task DisplayPrimeCountAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(await GetPrimesCountAsync(i*1000000+2,1000000));
            }
        }

        //本节内容复杂难懂，主要是阐述await的工作形式，优势，在UI工程中的应用等
    }
}
