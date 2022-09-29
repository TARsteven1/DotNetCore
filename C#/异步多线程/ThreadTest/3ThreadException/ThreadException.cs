using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTest._3ThreadException
{
    class ThreadException
    {
        #region 正常情况下无法捕获到线程异常
        static void Main()
        {
            try
            {
                new Thread(Go).Start();
            }
            catch (Exception)
            {
                Console.WriteLine("正常情况下无法捕获到线程异常");
            }
        }
        static void Go()
        {
            throw null;//主动抛出一个空异常
        }
        #endregion

        #region 如何捕获线程异常
        static void HowToCatchThreadException()
        {
            new Thread(Go1).Start();

        }
        static void Go1()
        {
            try
            {
                throw null;//主动抛出一个空异常
            }
            catch (Exception e)
            {
                Console.WriteLine("捕获异常"+ e.Message);
            }
        }
        #endregion

        #region 其他
        //在 WPF、WinForm中可通过订阅全局异常处理事件：(但只能处理UI线程的异常，否则不会触发这些异常)
            //Application.DispatcherUnhandledException
            //Application.ThreadException
        //而任何线程由未处理的异常都会触发：
           // AppDomain.CurrentDomain.UnhandledException
        #endregion
    }
}
