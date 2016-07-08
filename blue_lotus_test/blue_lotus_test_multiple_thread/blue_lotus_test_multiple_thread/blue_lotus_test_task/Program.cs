using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace blue_lotus_test_task
{
    class Program
    {
        static object taskMethodLock = new object();
        static void TaskMethod(object title)
        {
            lock(taskMethodLock)
            {
                Console.WriteLine(title);
                Console.WriteLine("Task id : {0}, thread : {1}", 
                    Task.CurrentId == null ? "no task" : Task.CurrentId.ToString(),
                    Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("is pooled thread : {0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("is background thread: {0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }

        static void TasksUsingThreadPool()
        {
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "Using a task factory!");
            
            Task t2 = Task.Factory.StartNew(TaskMethod, "Factory via a task!");

            Task t3 = new Task(TaskMethod, "Using a task constructor and Start!");
            t3.Start();

            Task t4 = Task.Run(() => TaskMethod("Using the Run method!"));
        }

        private static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }

        private static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running", TaskCreationOptions.LongRunning);
            t1.Start();
        }

        private static void DoOnFirst()
        {
            Console.WriteLine("doing some task {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }

        private static void DoOnSecond(Task t)
        {
            Console.WriteLine("task {0} finished", t.Id);
            Console.WriteLine("this task id {0}", Task.CurrentId);
            Console.WriteLine("do some cleanup");
            Thread.Sleep(3000);
        }

        private static void DoOnError(Task t)
        {
            Console.WriteLine("task {0} finished", t.Id);
            Console.WriteLine("this task id {0}", Task.CurrentId);
            Console.WriteLine("do error!");
            Thread.Sleep(3000);
        }
        
        private static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();
            Thread.Sleep(1000);
            Console.WriteLine(parent.Status);
            Thread.Sleep(4000);
            Console.WriteLine(parent.Status);
        }

        private static void ParentTask()
        {
            Console.WriteLine("task is {0}", Task.CurrentId);
            var child = new Task(ChildTask);
            child.Start();
            Thread.Sleep(1000);
            Console.WriteLine("parent started child!");
        }

        private static void ChildTask()
        {
            Console.WriteLine("child");
            Thread.Sleep(10000);
            Console.WriteLine("child finished!");
        }

        static void Main(string[] args)
        {
            //TaskMethod("zjslj");
            //TasksUsingThreadPool();
            Console.WriteLine("========================================");
            //RunSynchronousTask();
            //LongRunningTask();
            //Task t1 = new Task(DoOnFirst);
            //Task t2 = t1.ContinueWith(DoOnSecond);
            //Task t3 = t1.ContinueWith(DoOnSecond);
            //Task t4 = t1.ContinueWith(DoOnError, TaskContinuationOptions.OnlyOnRanToCompletion);
            //t1.Start();
            ParentAndChild(); 
            Console.ReadKey();
        }
    }
}
