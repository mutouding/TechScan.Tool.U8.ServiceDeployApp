using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplTest
{
    class Program
    {
        static async void Main(string[] args)
        {
            Console.WriteLine($"当前主线程{System.Threading.Thread.CurrentThread.ManagedThreadId}开始");


            var val = await testfunc();
            Console.WriteLine($"当前主线程{System.Threading.Thread.CurrentThread.ManagedThreadId}结束");

        }

        static async Task<string> testfunc()
        {
            Console.WriteLine($"当前子线程{System.Threading.Thread.CurrentThread.ManagedThreadId}开始");

            var v = await Task.Run(() =>
              {
                  System.Threading.Thread.Sleep(5000);
                  return "我是testfunc";
              });

            Console.WriteLine($"当前子线程{System.Threading.Thread.CurrentThread.ManagedThreadId}结束");
            return v;
        }
    }
}
