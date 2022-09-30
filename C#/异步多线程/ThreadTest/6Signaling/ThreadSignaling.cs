using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTest._6Signaling
{
    class ThreadSignaling
    {
    /// <summary>
    /// 信号可以控制线程的状态，让其暂时挂起，随时等待开启
    /// </summary>
        static void Main()
        {
            var signal = new ManualResetEvent(false);//创建一个信号实例
           
            new Thread(()=> {
                Console.WriteLine("等待信号");
                signal.WaitOne();//让线程等待，此时线程属于阻塞状态
                signal.Dispose();//一旦WaitOne方法接收到信号后就调用Dispose
                Console.WriteLine("已获取信号");
            }).Start();

            Thread.Sleep(3000);
            signal.Set();//主线程发送信号给其他线程，让其继续运行

            signal.Reset();//此方法可以将分支线程再次关闭
        }
    }
}
