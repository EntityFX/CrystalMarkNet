using System;
using System.Threading;

namespace EntityFX.CrystalMarkNet.FPU
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

            double q = (1.0 / (32767 + 1.0));

            for (; ; )
            {
                for (i = 0; i < N; i+=4)
                {
                    holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
                    d[i] = q * holdrand;

                    holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
                    d[i + 1] = q * holdrand;

                    holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
                    d[i + 2] = q * holdrand;

                    holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
                    d[i + 3] = q * holdrand;
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