using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace TaskTest._2Continuation.TaskCompletionSource
{
    /// <summary>
    /// TaskCompletionSource
    /// · Task . Run 创建 Task 
    /// · 另一种方式就是用 TaskCompletionSource 来创建 Task 
    /// · TaskCompletionSource 让你在稍后开始和结束的任意操作中创建 Task 
    /// · 它会为你提供一个可手动执行的“从属” Task 
    /// · 指示操作何时结束或发生故障 
    /// · 它对 IO-Bound 类工作比较理想 
    /// · 可以获得所有 Task 的好处（传播值、异常、 Continuation 等） 
    /// · 不需要在操作时阻塞线程、
    /// </summary>
    class MyTaskCompletionSource
    {
        static void Main(string[] args)
        {
            var tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Thread.Sleep(5000);
                tcs.TrySetResult(42);
            })
            {
                IsBackground = true
            }.Start();

            Task<int> task = tcs.Task;
            Console.WriteLine(task.Result);

            //调用此方法相当于调用Task.Factory.StartNew
            //并使用TaskContinuationOptions.LongRunning的选项来创建非线程池的线程
            Task<int> task2 = Run(() =>
            {
                Thread.Sleep(5000);
                return 42;
            });
        }
        // 使用TaskCompletionSource
        // · 初始化一个实例即可
        // · 它有一个 Task 属性可返回一个 Task
        // · 该 Task 完全由 TaskCompletionsource 对象控制
        // · 调用任意一个方法都会给 Task 发信号：
        //   · 完成、故障、取消
        // · 这些方法只能调用一次，如果再次调用：
        //   · SetXxx 会抛出异常“
        //   · TryXxx 会返回false


        static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }).Start();
            return tcs.Task;
        }

        void TryDelay()
        {
            //Task.Delay 相当于异步版本的Trhead.Sleep
            Task.Delay(5000).GetAwaiter().OnCompleted(() => Console.WriteLine(42));
            Task.Delay(5555).ContinueWith(ant => Console.WriteLine(42));
        }
    }
}
