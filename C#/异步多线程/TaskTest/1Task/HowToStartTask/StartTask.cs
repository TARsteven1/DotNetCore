using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskTest._1Task.HowToStartTask
{
    class StartTask
    {
        static void Main(string[] args)
        {
            //开始一个Task，.Net4.5之后的形式，Task.Run()传入一个委托
            //Task创建默认为后台线程（当主线程结束时，所以后台线程都会强制结束）
            Task task=  Task.Run(() => Console.WriteLine("Foo"));

            Console.WriteLine(task.Status);//task的状态可提供追踪其执行状态
        }
        //·Task.Run 返回一个 Task 对象，可以使用它来监视其过程

        //·在 Task . Run 之后，我们没有调用 Start ，因为该方法创建的是“热”任务（ hot task )
        //  ．可以通过 Task 的构造函数创建“冷”任务（cold task ) ，但是很少这样做


    }
}
