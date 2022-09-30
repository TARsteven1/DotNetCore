using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest._9ThreadPool
{
    class WhatsThreadPool
    {
        /*
      线程池ThreadPool
       · 当开始一个线程的时候，将花费几百微秒来组织类似以下的内容：
         · 一个新的局部变量栈（ Stack )
       · 线程池就可以节省这种开销：
         · 通过预先创建一个可循环使用线程的池来减少这一开销。
      · 线程池对于高效的并行编程和细粒度并发是必不可少的 
      · 它允许在不被线程启动的开销淹没的情况下运行短期操作
      
      使用线程池线程需要注意的几点:
      · 不可以设置池线程的 Name 
      · 池线程都是后台线程 
      · 阻塞池线程可使性能降级 
      
      · 你可以自由的更改池线程的优先级 
        · 当它释放回池的时候优先级将还原为正常状态
        · 可以通过 Thread . CurrentThread . IsThreadPoolThread 属性采判断是否执行在池线程上
      
      
      进入线程池 · 最简单的、显式的在池线程运行代码的方式就是使用 Task.Run 
      //Task is inSystem.Threading.Tasks
      Task.Run( ( ）=＞ Console ．WriteLine ( " Hello from the thread pool " ) );
      
      谁使用了线程池 
      ·WCF 、 Remoting 、 ASP . NET 、 ASMX Web Services 应用服务器 
      ·System . Timers ．Timer 、 System . Threading ．Timer 
      ·并行编程结构 
      ·Backgroundworker类（现在很多余）
      ·异步委托（现在很多余）
      
      线程池中的整洁
      ·线程池提供了另一个功能，即确保临时超出 计算-Bound 的工作不会导致CPU超额订阅 
      ·CPU 超额订阅：活跃的线程超过 CPU 的核数，操作系统就需要对线程进行时间切片
      ·超额订阅对性能影响很大，时间切片需要昂贵的上下文切换，并且可能使 CPU 缓存失效，而 CPU 缓存对于现代处理器的性能至关重要
      
      线程池中的整洁 CLR 的策略 
      · CLR 通过对任务排队并对其启动进行节流限制来避免线程池中的超额订阅。 
      · 它首先运行尽可能多的并发任务（只要还有 CPU 核），然后通过爬山算法调整并发级别，并在特定方向上不断调整工作负载。
        · 如果吞吐量提高，它将继续朝同一方向（否则将反转）。 
      · 这确保它始终追随最佳性能曲线，即使面对计算机上竞争的进程活动时也是如此 
      · 如果下面两点能够满足，那么 CLR 的策略将发挥出最佳效果： 
        · 工作项大多是短时间运行的（ < 250 毫秒，或者理想情况下＜100 毫秒），因此 CLR 有很多机会进行测量和调整。
        · 大部分时间都被阻塞的工作项不会主宰线程池
         */
        static void Main()
        {
            Task.Run(() => Console.WriteLine(" Hello from the thread pool "));
        }

    }
}
