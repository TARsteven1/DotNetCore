using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace TaskTest._1Task.LongRunningTasks
{
    class MyLongRunningTask
    {
        static void Main(string[] args)
        {
            //开始一个Task，.Net4.5之前的形式，Task.Factory.StartNew()传入一个委托
            Task task = Task.Factory.StartNew(() => {
                Thread.Sleep(5000);
                Console.WriteLine("Foo");
            },TaskCreationOptions.LongRunning);//次方式提供重载可设置LongRunning

            Console.WriteLine("当前Task是否已完成：" + task.IsCompleted);//fasle

            task.Wait();//阻塞直至task完成操作,类似Thread的Join
            Console.WriteLine("当前Task是否已完成：" + task.IsCompleted);//true

            /*
             Long-running tasks 长时间运行的任务
            · 默认情况下， CLR 在线程池中运行 Task ，这非常适合短时间运行的 Compute-Bound 类工作。
            · 针对长时间运行的任务或者阻塞操作，你可以不采用线程池,而使用 TaskCreationOptions. LongRunning
            · 如果同时运行多个 Iong 一 running tasks （尤其是其中有处于阻塞状态的），那么性能将会受很大影响，这时有比 TaskCreationOptions. LongRunning 更好的办法：
               · 如果任务是 IO- Bound , TaskCompletionsource 和异步函数可以让你用回调 ( Coninuations ）代替线程来实现并发。
               · 如果任务是 Compute-Bound ，生产者／消费者队列允许你对任务的并发性进行限流，避免把其他线程和进程饿死。
             */
        }
    }
}
