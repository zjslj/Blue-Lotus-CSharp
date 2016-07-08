using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blue_lotus_test_threadpoll
{
    class Program
    {
        public struct Data
        {
            public string Message;
        }
        static void ThreadMainWithParameters(object o)
        {
            Data d = (Data)o;
            Console.WriteLine("Running in a thread, received {0}", d.Message);
        }

        #region main
        static void Main(string[] args)
        {
            //int WorkerThreads;
            //int CompletionPortThreads;
            //ThreadPool.GetMaxThreads(out WorkerThreads, out CompletionPortThreads);
            //Console.WriteLine("Max worker threads: {0}, I/O completion threads : {1}", WorkerThreads, CompletionPortThreads);
            //for (int i = 0; i < 5; i++ )
            //{
            //    ThreadPool.QueueUserWorkItem(JobForAthread);
            //}
            //Thread.Sleep(3000);
            //var t1 = new Thread(ThreadMain);
            //t1.Start();
            //Thread.Sleep(1000);
            //var t1 = new Thread(() => Console.WriteLine("runnign in a thread, id:{0}", Thread.CurrentThread.ManagedThreadId));
            //t1.Start();
            var d = new Data { Message = "Info" };
            var t1 = new Thread(ThreadMainWithParameters);
            t1.Start(d);
            Console.WriteLine("This is the main thread");
            Console.WriteLine("-----------------------------");
            var obj = new Mythread("my thread");
            var t2 = new Thread(obj.ThreadMain);
            t2.Start();
            Console.WriteLine("-----------------------------");
            var t3 = new Thread(ThreadMain) { Name = "MyNewThread", IsBackground = true };
            t3.Start();
            Console.ReadKey();
        }
        #endregion
        static void ThreadMain()
        {
            Console.WriteLine("Running in a thread!");
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} completed!", Thread.CurrentThread.Name);
        }
        static void JobForAthread(object state)
        {
            for (int i = 0; i < 3; i++ )
            {
                Console.WriteLine("Loop {0}, running inside pooled thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }
        }
    }

    public class Mythread
    {
        private string data;

        public Mythread(string data)
        {
            this.data = data;
        }
        public void ThreadMain()
        {
            Console.WriteLine("Running in a thread, data: {0}", data);
        }
    }
}
