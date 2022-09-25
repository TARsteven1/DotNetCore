using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text.RegularExpressions;

namespace TryBenchmarkDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("使用BenchmarkDotNet进行性能测试!");
            //运行后会让系统运行至最佳状态再去进行性能测试
            BenchmarkRunner.Run<BMT>();
            Console.Write("按任意键退出...");
            Console.ReadKey(true);

        }
    }
   /// <summary>
   /// 用三个函数进行性能比较
   /// </summary>
   /// 内存监听
    [MemoryDiagnoser]
    public class BMT
    {
        private string _str = "林柳 蓝鲸 一双混";
        [Benchmark]
        public int Find1() {
            //正则表达式
            Regex regex = new(@"\b\w+\b");
            return regex.Matches(_str).Count;
        }        
            Regex regex1 = new(@"\b\w+\b");
        [Benchmark]
        public int Find2() {
            return regex1.Matches(_str).Count;
        } 
        [Benchmark]
        public int Find3() {
            //使用静态搜索
            return Regex.Matches(_str, @"\b\w+\b").Count;
        }       
        [Benchmark]
        public int Find4() {
            //主动申请一些内存
            var test = new int[10];
            //使用静态搜索
            return Regex.Matches(_str, @"\b\w+\b").Count;
        }
    }
}
