using System;
using System.Threading;

namespace ThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(WriteY);//开辟新线程,将委托传入线程中
            thread.Name = "Y Thread...";
            thread.Start();//开始运行线程

            thread.Join();//等待thread线程结束后再继续后续代码，有2个重载，可用于判断线程超时
            Thread.Sleep(4000);//暂停当前线程，等待4秒
            Thread.Yield();//等于 Thread.Sleep(0);用于诊断


            //同时主线程也再工作
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("X");
            }
        }
        static void WriteY()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Y");
            }
        }
        static void Doc()
        {
            //线程的一些属性说明
            Thread thread = new Thread(WriteY);

            //一旦开始就是true，线程结束就是fasle（传入线程的委托结束后），线程结束后无法重启
            Console.WriteLine(thread.IsAlive);
            //线程名，用于调试，只能设置一次，再改回抛出异常
            thread.Name = "Y Thread...";
            //静态属性，返回当前执行的线程
            Console.WriteLine(Thread.CurrentThread);
            //线程的状态
            Console.WriteLine(thread.ThreadState);
        }

        static void Block()
        {
            Thread thread = new Thread(WriteY);
            //由于线程状态是flag 枚举，所以：
            //判断线程的状态是否为等待，要这样写
            if ((thread.ThreadState | ThreadState.WaitSleepJoin )!= 0)
            {
                Console.WriteLine("thread的状态为阻塞");
            }

        }
       /// <summary>
       /// 判断传入的线程状态并返回
       /// </summary>
       /// <param name="ts"></param>
       /// <returns></returns>
        public static ThreadState SimpleThreadState(ThreadState ts)
        {
            return ts & (ThreadState.Stopped | ThreadState.Unstarted | ThreadState.WaitSleepJoin);
        }
    }
}
