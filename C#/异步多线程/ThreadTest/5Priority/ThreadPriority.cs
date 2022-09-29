using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ThreadTest._5Priority
{
    class ThreadPriority
    {
        static void Main()
        {
            Thread worker = new Thread(() => Console.ReadLine());//等待输入
          
                Console.WriteLine(worker.Priority.ToString());//获得线程的优先级（返回枚举结果）

        }
        static void LevelUp()
        {
            //提升线程优先级可能会饿死其他线程
            //提升线程所在进程的优先级
            using (Process p = Process.GetCurrentProcess())//获取当前进程
                p.PriorityClass = ProcessPriorityClass.High;//提升进程优先级，最高是realtime
        }
    }
}
