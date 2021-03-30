using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CrystalMarkNet
{
    abstract class CrystalBenchmarkBase : ICrystalBenchmark
    {

        public virtual int Bench(int threads)
        {
            return BenchAllTask(threads, BenchImplementation);
        }

        public virtual string Name => GetType().Name;

        protected abstract int BenchImplementation(CancellationToken cancellationToken);

        static int BenchAllTask(int threads, Func<CancellationToken, int> activity)
        {
            Task<int>[] tasks = new Task<int>[threads];

            for (int c = 0; c < threads; c++)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                cancelTokenSource.CancelAfter(10000);
                CancellationToken token = cancelTokenSource.Token;
                tasks[c] = Task.Run(() =>
                    {
                        try
                        {
                            return activity(token);
                        }
                        catch (Exception)
                        {
                            return 0;
                        }
                    }
                );
            }

            Task.WaitAll(tasks);

            return tasks.Sum(t => t.Result);
        }
    }
}