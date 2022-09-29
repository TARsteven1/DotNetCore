using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ThreadTest._4ForegroundBackground.What
{
    class FBThreadSample
    {
        //默认，手动创建的线程就是前台线程
        //只要还有前台线程，那么这个程序就一直处于活跃的状态，无法正常关闭；但后台线程却不行
       
        static void Main(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());//等待输入

            if (args.Length>0)
            {
                worker.IsBackground = true;//变为后台线程
            }
            worker.Start();
        }
    }
}
