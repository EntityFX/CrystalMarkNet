using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFX.CrystalMarkNet
{
    abstract class CrystalBenchmarkBase : ICrystalBenchmark
    {
        public const int DefaultTime = 10000;

        protected int benchTime = DefaultTime;
        protected Stopwatch stopwatch = new Stopwatch();

        public virtual double Bench(int threads, int benchTime = DefaultTime)
        {
            this.benchTime = benchTime;
            return BenchAllTask(threads, BenchImplementation);
        }

        public virtual string Name => GetType().Name;

        protected abstract double BenchImplementation(CancellationToken cancellationToken);

        private double BenchAllTask(int threads, Func<CancellationToken, double> activity)
        {
            Task<double>[] tasks = new Task<double>[threads];
            stopwatch.Start();
            for (int c = 0; c < threads; c++)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                cancelTokenSource.CancelAfter(benchTime);
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

            var result = tasks.Sum(t => t.Result);

            var score = result / stopwatch.Elapsed.TotalSeconds;
            stopwatch.Stop();
            return score;
        }
    }
}