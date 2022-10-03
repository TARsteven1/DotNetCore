using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest._1Task.Exception
{
    //Task 的异常 
    //    · 与Thread 不一样， Task 可以很方便的传播异常 
        
    class MyTaskException
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() => {
                throw null;
            });

            try
            {
                //· 如果你的 task 里面抛出了一个未处理的异常（故障），那么该异常就会重新被抛出给： 
                     //· 访问了Task<TResult> 的 Result 属性的地方。
                     //· 调用了Wait( ）的地方 
                task.Wait();
            }
            catch (AggregateException e)
            {
                 //· CLR 将异常包裹在 AggregateExcePtion 里，以便在并行编程场景中发挥很好的作用。
                if (e.InnerException is NullReferenceException)
                {
                    Console.WriteLine("空引用");
                }
                else
                {
                throw;
                }
                /*
                Task 的异常 
                · 无需重新抛出异常，通过 Task 的 IsFaulted 和 IsCanceled 属性也可以检测出 Task 是否发生了故障：
                · 如果两个属性都返回 felse ，那么没有错误发生。 
                  · 如果 IsCanceled 为 true ，那就说明一个 operationCanceledException 为该 Task 抛出了。 
                  · 如果 IsFaulted 为 true ，那就说明另一个类型的异常被抛出了，而 ExcePtion 属性也将指明错误。*/
                Console.WriteLine(task.IsCanceled);
                Console.WriteLine(task.IsFaulted);
            }
         /* 异常与“自治”的 Task 
            · 自治的，“设置完就不管了”的 Task 。就是指不通过调用 Wait( ）方法、 Resuit 属性或 continuation 进行会合的任务。 
              · 针对自治的 Task ，需要像 Thread 一样，显式的处理异常，避免发生“悄无声息的故障”。 
              · 自治 Task 上未处理的异常称为未观察到的异常。
            未观察到的异常
            · 可以通过全局的 TaskScheduler . UnobservedTaskException 来订阅未观察到的异常。 
            · 关于什么是“未观察到的异常”，有一些细微的差别： 
              · 使用超时进行等待的 Task ，如果在超时后发生故障，那么它将会产生一个“未观察到的异常”。 
              · 在 Task 发生故障后，如果访问 Task 的 ExcePtion 属性，那么该异常就被认为是“已观察到的”。
             */
        }
    }
}
