using System;
using System.Threading;

namespace EntityFX.CrystalMarkNet.FPU
{
    class FFT : CrystalBenchmarkBase
    {
        private const int N = 128;
        protected override double BenchImplementation(CancellationToken cancellationToken)
        {
            ushort count = 0;
            const float PI = (float)Math.PI;

            int i, j, k;

            float[] fr = new float[N];
            float[] fi = new float[N];

            float w, wr, wi, x, xr, xi;

            for (; ; )
            {
                for (i = 0; i < N; i++)
                {
                    x = -2 * PI + (float)i * 4.0f * PI / (float)N;
                    fr[i] = (float)(Math.Sin(x) + Math.Sin(2.0f * x));
                    fi[i] = 0.0f;
                }

                Bitreverse(ref fr, N);
                Bitreverse(ref fi, N);

                for (k = 1; k < N; k = k * 2)
                {
                    for (j = 0; j < k; j++)
                    {
                        w = 2.0f * (float)PI / (2.0f * (float)k);
                        wr = (float)Math.Cos(w * j);     
                        wi = (float)-Math.Sin(w * j);
                        for (i = j; i < N; i += (k * 2))
                        {
                            xr = wr * fr[i + k] - wi * fi[i + k];
                            xi = wr * fi[i + k] + wi * fr[i + k];
                            fr[i + k] = fr[i] - xr;
                            fi[i + k] = fi[i] - xi;
                            fr[i] = fr[i] + xr;
                            fi[i] = fi[i] + xi;
                        }
                    }
                }
                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return (double)count * N / 1153.3980d * 25;
                }
            }
        }

        private static void Bitreverse(ref float[] a, int N)
        {
            int i, j, k, l;
            float[] b = new float[N];

            for (i = 0; i < N; i++)
            {
                k = 0;
                l = N / 2;
                for (j = 1; j < N; j *= 2)
                {
                    if ((i & j) == 1) k += l;
                    l /= 2;
                }
                b[i] = a[k];
            }
            for (i = 0; i < N; i++)
            {
                a[i] = b[i];
            }
        }
    }
}