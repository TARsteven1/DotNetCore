using System;
using System.Windows;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ThreadTest._7RichClientAppThread
{
    class WPF
    {
        /// <summary>
        /// 在Wpf等程序项目中，解决程序卡死（阻塞主线程）的方法是用创建线程处理消息等待的方式
        /// 但新线程无法直接处理UI控件，因为他们是被主线程所拥有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender,RoutedEventArgs e)
        {

            new Thread(Work).Start();
        }

        private void Work(object obj)
        {
            Thread.Sleep(3000);
            TxtMessage.Text = "要改变输入框的内容";//此时会报错，因为输入框是被主线程所拥有的
            UpdateMessage("要改变输入框的内容");//正确方法，使用Dispatcher.BeginInvoke(action);相当于把委托发送到UI线程的消息队列中等待执行
        }

        private void UpdateMessage(string msg)
        {
            Action action = () => TxtMessage.Text = msg;
            Dispatcher.BeginInvoke(action);
        }
    }
    //以下只是临时用来模拟WPF环境的无用内容
    internal class Dispatcher
    {
        internal static void BeginInvoke(Action action)
        {
            throw new NotImplementedException();
        }
    }

    internal class TxtMessage
    {
        public static string Text { get; internal set; }
    }

    internal class RoutedEventArgs
    {

    }
}
