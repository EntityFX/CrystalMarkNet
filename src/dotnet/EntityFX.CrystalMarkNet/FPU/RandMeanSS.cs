using System;
using System.Threading;

namespace EntityFX.CrystalMarkNet.FPU
{
    class RandMeanSS : CrystalBenchmarkBase
    {
        private const int N = 128;

        protected override double BenchImplementation(CancellationToken cancellationToken)
        {
            int i;
            double x, s1, s2;

            double[] d = new double[N];
  
            int count = 0;

            var rnd = new Random((int)DateTimeOffset.Now.ToUnixTimeSeconds());

            for (; ; )
            {
                for (i = 0; i < N; i++)
                {
                    d[i] = (double)rnd.Next() / 1000.0;
                }


                s1 = s2 = 0.0f;
                for (i = 1; i <= N; i++)
                {
                    x = d[i - 1];
                    x -= s1;
                    s1 += x / i;
                    s2 += (i - 1) * x * x / i;
                }
                s2 = Math.Sqrt(s2 / (N - 1));
                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return count / 40.1604;
                }
            }
        }
    }
}