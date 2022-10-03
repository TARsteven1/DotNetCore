using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace TaskTest._1Task.RetrunValue
{
    class TaskReturnValue
    {
        /*
        Task 的返回值 
            · Task 有一个泛型子类叫做 Task<TResuit> ，它允许发出一个返回值。 
            · 使用 Func ＜TResult ＞委托或兼容的 Lambda 表达式来调用 Task.Run 就可以得到 Tssk < TResult >。 
            · 随后，可以通过 Result 属性来获得返回的结果。如果这个 task 还没有完成操作，访问 Resuit 属性会阻塞该线程直到该 task 完成操作。
            · Task<TResult> 可以看作是一种所谓的“未来 / 许诺” (future 、 promise ) ，在它里面包裹着一个 Result ，在稍后的时候就会变得可用。
            · 在 CTP 版本的时候， Task<TResult ＞实际上叫做 Future<TResult>
        */

        static void Main(string[] args)
        {
            Task<int> task = Task.Run(() => {
                Thread.Sleep(5000);
                Console.WriteLine("Foo");
                return 3;
            });


           int result= task.Result;//阻塞直至task完成操作并顺利返回结果
            Console.WriteLine("当前Task的返回值：" + result.ToString());//3

        }
    }
}
