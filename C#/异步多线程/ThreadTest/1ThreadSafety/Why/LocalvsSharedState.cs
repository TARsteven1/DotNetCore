using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ThreadTest._1ThreadSafety.Why
{
    class LocalvsSharedState
    {
        static void Main()
        {

        }
        #region Local本地独立：CLR为每个线程分配自己的内存栈（Stack），以便本地变量保持独立
        static void Local()
        {
            new Thread(Go).Start();//新线程调用Go
            Go();
        }
        static void Go()
        {
            //cycles是本地变量
            //再每个线程的内存栈上都会创建cycles的独立副本
            for (int cycles = 0; cycles < 5; cycles++)
            {
                Console.WriteLine("？");
            }
        }
        //结果输出10个？
        #endregion

        #region Shared共享1：多个线程共享同一个实例
        bool _done;
        static void SharedInstance()
        {
            LocalvsSharedState lvs=  new LocalvsSharedState();//创建共有的实例
            new Thread(lvs.Run).Start();//新线程调用Run

            lvs.Run();
        }
         void Run()//实例方法
        {
            if (!_done)
            {
                _done = true;
                Console.WriteLine("Done");
            }
        }
        //结果只显示一个Done，两个线程使用共用的实例调用的方法，所以使用的是同一个_done
        #endregion
        
        #region Shared共享2：被Lambda表达式或匿名委托所捕获的本地变量，会被编译器转化为字段(field),所以也会被共享
        static void SharedDelegate()
        {
            bool done=false;
            ThreadStart action = () =>
            {
                if (!done)
                {
                    done = true;
                    Console.WriteLine("Done");
                }
            };
            new Thread(action).Start();//新线程调用Run

            action();
        }
        //结果只显示一个Done
        #endregion

        #region Shared共享3：静态字段(field)也会在线程间共享数据
           static bool done;//静态字段在同一个作用域下被所有线程共享
        static void SharedStaticField()
        {

            new Thread(Run1).Start();//新线程调用Run

            Run1();
        }
        static void Run1()//实例方法
        {
            if (!done)
            {
                done = true;
                Console.WriteLine("Done");
            }
        }
        //结果只显示一个Done
        #endregion
    }

    //结论：Shared共享状态缺乏线程安全，由于输出无法确定（有可能输出两次Done），尽量避免使用共享状态
}
