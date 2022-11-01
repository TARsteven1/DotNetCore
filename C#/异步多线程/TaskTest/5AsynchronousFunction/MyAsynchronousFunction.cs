using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace TaskTest._5AsynchronousFunction
{
    class MyAsynchronousFunction
    {
        //如何写异步函数：
        //void用Task替换，有返回值的用Task<TResult>对应类型返回值替换

      static async Task Main(string[] srgs)
        {
            //匿名方法，通过async变成异步方法的方式
            Func<Task> unnamed = async () =>
             {
                 await Task.Delay(1000);
                 Console.WriteLine("Done!");
             };
            //调用
            await unnamed();
            await NamedMethod();

            //含参的匿名方法转变写法
            Func<Task<int>> unnamedvalue = async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine("Done!");
                return 123;
            };
            //调用
            int answer = await unnamedvalue();
        }
        //一个标准的异步函数
        static async Task NamedMethod()
        {
            await Task.Delay(1000);
        }
    }
}
