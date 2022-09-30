using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTest._8SynchronizationContexts
{
    class MySynchronizationContexts
    {

        SynchronizationContext synchronizationContext;//抽象类，使得Thread Marshaling（把一个数据的所有权从一个线程交到另一个线程）得到泛化
            //把C#数据转化成Json的过程可称为Marshaling

        public void MainWindow()//WPF的构造函数
        {
            //为当前的UI线程捕获了Synchronization Context
            synchronizationContext = SynchronizationContext.Current;
            new Thread(Work).Start();

        }
        private void Work(object obj)
        {
            Thread.Sleep(5000);
            UpdateMessage("要改变输入框的内容");//正确方法，使用Dispatcher.BeginInvoke(action);相当于把委托发送到UI线程的消息队列中等待执行
        }

        private void UpdateMessage(string msg)
        {
            //把委托Marshal给UI线程
            synchronizationContext.Post(_ => TxtMessage.Text = msg, null);

            //调用Post就相当于调用 Dispatcher或Control上面的BeginInvoke方法
            //调用Send就相当于调用 Dispatcher或Control上面的Invoke方法

        }


    }
    //以下只是临时用来模拟WPF环境的无用内容
    internal class TxtMessage
    {
        public static string Text { get; internal set; }
    }
}
