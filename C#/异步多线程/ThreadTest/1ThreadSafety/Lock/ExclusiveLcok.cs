using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTest._1ThreadSafety.Lock
{
    class ExclusiveLcok
    {
        static bool _done;
        static readonly object _locker = new object();

        static void Main()
        {
            new Thread(Go).Start();
            Go();
        }
        static void Go()
        {//使用lock语句加锁
            lock (_locker)//锁可以给予任何引用类型对象
            {
                if (!_done)
                {
                    Console.WriteLine("Done");
                    _done = true;
                }
            }
            //当人多个线程同时竞争一个锁的时候，一个线程回等待或阻塞，直至锁变成可用状态
            //在多线程上下文中，以此种方式避免避免不确定性的代码就叫 线程安全
            //lock不是线程安全的银弹，容易忘记对字段加锁，lock也会引起死锁问题
        }
    }
}
