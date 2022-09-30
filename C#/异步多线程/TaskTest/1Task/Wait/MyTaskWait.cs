using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace TaskTest._1Task.Wait
{
    class MyTaskWait
    {
        static void Main(string[] args)
        {
            Task task= Task.Run(() => {
                Thread.Sleep(5000);
                Console.WriteLine("Foo");
                });

            Console.WriteLine("当前Task是否已完成："+task.IsCompleted);//fasle

            task.Wait();//阻塞直至task完成操作,类似Thread的Join
            Console.WriteLine("当前Task是否已完成：" + task.IsCompleted);//true

            //Wait也可以提供设置一个超时时间和一个取消令牌来提前结束等待
        }
    }
}
