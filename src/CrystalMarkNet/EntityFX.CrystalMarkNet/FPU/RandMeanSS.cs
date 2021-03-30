using System;
using System.Threading;

namespace CrystalMarkNet.FPU
{
    class RandMeanSS : CrystalBenchmarkBase
    {
        private const int N = 128;

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            int i;
            double x, s1, s2;

            double[] d = new double[N];
            long holdrand = 1;
            int count = 0;

            for (; ; )
            {
                for (i = 0; i < N; i++)
                {
                    holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
                    d[i] = (double)((1.0 / (32767 + 1.0)) * holdrand);
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
                    return (int)(count / 90 / 20);
                }
            }
        }
    }
}