using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadSafetyDemo
{
    class Program
    {
        const int a = 1000;
        static int d = 0;
        static readonly object _object = new object();

 
        static void Main(string[] args)
        {
            Thread obj = new Thread(() => Division(0));
            obj.Start(); //2-nd thread

            Division(1);  //1-st current thread

            Console.WriteLine();
            Console.WriteLine("Thread job finished. Please press any key");
            Console.ReadKey();
        }


        private static void Division(int threadN = 0)
        {
            for (int i = 0; i <= 100; i++)
            {
                try
                {
                    //Uncomment monitor to fix collisions
                    //Monitor.Enter(_object);

                    var r = new Random();
                    d = r.Next(1, 10);
                    Console.Write("Thread #{0}; i={1}; d={2}...", threadN, i, d);

                    double answer = a / d;
                    Console.WriteLine("...Thread #{0}; i={1}; d={2}; answer={3}", threadN, i, d, answer);
                    d = 0;

                    int sleep = r.Next(20);
                    Thread.Sleep(sleep); // let's give it a better chance to switch to zero

                    //Monitor.Exit(_object);

                }
                catch (DivideByZeroException e)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("--- Thread collision! --- Divide By Zero Exception happened in thread #" + threadN);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
