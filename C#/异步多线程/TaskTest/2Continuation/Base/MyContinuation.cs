using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest._2Continuation.Base
{
 /// <summary>
 /// Continuation
 //· 一个 Continuation 会对 Task 说：“当你结束的时候，继续再做点其它的事” 
 //· Continuation 通常是通过回调的方式实现的
 //  · 当现作一结束．就开始执行
/// </summary>
    class MyContinuation
    {
        static void Main(string[] args)
        {
            Task<int> primNumberTask = Task.Run(() =>
            Enumerable.Range(2,3000000).Count(n=>
            Enumerable.Range(2,(int)Math.Sqrt(n) - 1).All(i=> n % i >0)));//一个求2-300000之间质数个数的方法，在此主要用于拖延时间
            
            //在 task 上调用 GetAwaiter 会返回一个 awaiter 对象它的 OnCompleted 方法会告诉之前的 task : “当你结束／发生故障的时候要执行委托”。
            var awaiter = primNumberTask.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                int result = awaiter.GetResult();
                Console.WriteLine("结果的数量为:"+ result);
            });
            //也可以将 Continuation 附加到已经结束的 task 上面，此时 Continuation 将会被安排立即执行。

            Console.ReadLine();//主动阻塞主线程
        }
        /*
          对awaiter的说明
           · 任何可以暴露下列两个方法和一个属性的对象就是 awaiter :
             · OnCompleted
             · GetResult
             · 一个叫做 IsCompleted 的 bool 属性 
           · 没有接口或者父类来统一这些成员。
           · 其中 OnCompleted 是 INotifyCompletion 的一部分*/

        //  非泛型 task
        //· 针对非泛型的 task, GetResuIt ( ）方法有一个 void 返回值，它就是用来重新抛出异常。

        /*
        同步上下文
         · 如果同步上下文出现了，那么 OnCompleted 会自动捕获它，并将 Continuation 提交到这个上下文中。这一点在富客户端应用中非常有用，因为它会把 Continuation 放回到 UI 线程中。
         · 如果是编写一个库，则不希望出现上述行为，因为开销较大的 UI 线程切换应该在程序运行离开库的时候只发生一次，而不是出现在方法调用之间。所以，我们可以使用ConfigureAwait方法来避免这种行为：  //  var awaiter = primNumberTask.ConfigureAwait(false).GetAwaiter();
         · 如果没有同步上下文出现，或者你使用的是 ConfigureAwait( false ) ，那么Continuation会运行在先前 task 的同一个线程上，从而避免不必要的开销*/


        static void MyContinueWith()// 另外一种附加 Continuation的方式就是调用 task 的 continuewith 方法
        {
            Task<int> primNumberTask = Task.Run(() =>
           Enumerable.Range(2, 3000000).Count(n =>
           Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));//一个求2-300000之间质数个数的方法，在此主要用于拖延时间

            //· ContinueWith本身返回一个 task ，它可以用它来附加更多的 Continuation 。
            primNumberTask.ContinueWith(task =>
            {
                int result = task.Result;
                Console.WriteLine("结果的数量为:" + result);
            });
            /* 必须直接处理AggregateException：
             · 如果 task 发生故障，需要写额外的代码来把 Continuation 封装（ marshal ）到 UI 应用上。
             · 在非 UI 上下文中，若想让 Continuation 和 task 执行在同一个线程上，必须指定 TaskContinuationOptions.ExecuteSynchronously ，否则它将弹回到线程池。*/

            //· Continuewith 对于并行编程来说非常有用。
        }
    }



}
