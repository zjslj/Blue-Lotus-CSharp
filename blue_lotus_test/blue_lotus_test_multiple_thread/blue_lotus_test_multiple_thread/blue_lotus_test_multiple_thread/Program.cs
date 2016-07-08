using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace blue_lotus_test_multiple_thread
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ParallelLoopResult result = Parallel.For(0, 10, async i =>
                                        {
                                            Console.WriteLine("{0}, task:{1}, thread:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                                            //Thread.Sleep(10);
                                            await Task.Delay(10);
                                            Console.WriteLine("{0}, task:{1}, thread:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                                        });
            Console.WriteLine("is completed : {0}", result.IsCompleted);
            */
            ParallelLoopResult result = Parallel.For(10, 40, async (int i, ParallelLoopState pls) =>
            {
                Console.WriteLine("i: {0} task {1}", i, Task.CurrentId);
                await Task.Delay(10);
                if(i > 11)
                {
                    pls.Break();
                }
            });
            Console.WriteLine("Is completed: {0}", result.IsCompleted);
            Console.WriteLine("lowest break iteration: {0}", result.LowestBreakIteration);

            Parallel.For<string>(0, 20, () =>
            {
                Console.WriteLine("init thread {0}, task {1}",
                        Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                return String.Format("t:{0}", Thread.CurrentThread.ManagedThreadId);
            }, (i, pls, str1) =>
            {
                Console.WriteLine("body i {0} str1 {1} thread {2} task {3}", i, str1,
                        Thread.CurrentThread.ManagedThreadId, Task.CurrentId);
                Thread.Sleep(10);
                return String.Format("i {0}", i);
            },
            (str1) => 
            {
                Console.WriteLine("finally {0}", str1);
            });
            Console.WriteLine("============================================");
            string[] data = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
            Parallel.ForEach<string>(data, s => 
            {
                Console.WriteLine("Foreach test: {0}", s);
            });
            Console.WriteLine("============================================");
            ParallelInvoke();
            Console.ReadKey();
        }

        private static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }
        private static void Foo()
        {
            Console.WriteLine("Foo");
        }
        private static void Bar()
        {
            Console.WriteLine("Bar");
        }

    }
}
