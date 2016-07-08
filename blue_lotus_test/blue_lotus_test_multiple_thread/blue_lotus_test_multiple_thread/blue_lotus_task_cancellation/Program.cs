using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace blue_lotus_task_cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** token canceled!"));
            cts.CancelAfter(500);

            try
            {
                ParallelLoopResult result = Parallel.For(0, 100, new ParallelOptions()
                    {
                        CancellationToken = cts.Token,
                    },
                    x => 
                    {
                        Console.WriteLine("loop {0} started", x);
                        int sum = 0;
                        for(int i=0; i<100; i++)
                        {
                            Thread.Sleep(2);
                            sum += i;
                        }
                        Console.WriteLine("loop {0} finished", x);
                    });
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine("exception:{0}", ex.Message);
            }

            Console.ReadKey();
        }
    }
}
