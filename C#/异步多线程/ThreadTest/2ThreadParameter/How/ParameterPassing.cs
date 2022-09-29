using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTest._2ThreadParameter.How
{
    class ParameterPassing
    {
        #region 最简单的传参方法（使用Lambda表达式）
        static void TheMostEasyWay2PassPara2Thread()
        {
            Thread t = new Thread(() => Print("Pass String"));
            t.Start();
            //也可以整个都写在Lambda里
            new Thread(() => { Console.WriteLine("Pass String Aging"); }).Start();
        }
        static void Print(string str)
        {
            Console.WriteLine(str);
        }
        #endregion

        #region C#3.0前
        static void LastCSharp3Dot0()
        {
            //3.0以前没有Lambda
            Thread t = new Thread(Print1);
            t.Start("Pass String");//在此传参
        }
        static void Print1(object str)
        {
            string temp = (String)str;
            Console.WriteLine(temp);
        }
        //Thread的重载构造函数可接受下列两个委托之一作为参数：
        //public delegate void ThreadStart();
        //public delegate void ParameterizedThreadStart(object obj);
        #endregion

        #region 如何保证Lambda不修改被捕获的变量？（造成的原因是见：1ThreadSafety-Why-52行）
        static void KeepValueInLambda()
        {
            for (int i = 0; i < 10; i++)
            {
                int temp = i;//Key Point
                new Thread(() => { Console.Write(temp); }).Start();
            }
            //这样输出的结果就不会被修改，但顺序是乱的（输出1-10）
        }
        #endregion
    }
}
