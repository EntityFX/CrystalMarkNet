using System.Threading;

namespace CrystalMarkNet.ALU
{
    class Napierian : CrystalBenchmarkBase
    {
        private const int N = 225;
        private const int M = 250;
        private const ushort RADIXBITS = 15;

        private const ushort RADIX = (1 << RADIXBITS);

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            int count = 0;
            ushort[] a = new ushort[N + 1];
            ushort[] t = new ushort[N + 1];

            for (; ; )
            {
                int m;
                uint k;
                for (m = 0; m <= N; m++)
                {
                    a[m] = t[m] = 0;
                }
                a[0] = 2;
                a[1] = t[1] = RADIX / 2;
                k = 3; m = 1;
                while ((m = Divs(m, t, k, t)) <= N)
                {
                    Add(ref a, ref t, ref a);
                    ++k;

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return (int)(count * 3.4) / 10;
                    }
                }
                //	print(a);
                count++;
            }
        }

        private static int Divs(int m, ushort[] a, uint x, ushort[] b)
        {
            int i;
            ulong t = 0;

            for (i = m; i <= N; i++)
            {
                t = (t << RADIXBITS) + a[i];
                b[i] = (ushort)(t / x);
                t %= x;
            }

            if (2 * t >= x)
            {
                for (i = N; (++b[i] & RADIX) == 1; i--)
                {
                    b[i] &= RADIX - 1;
                }
            }
            return (b[m] != 0) ? m : (m + 1);
        }

        private static void Add(ref ushort[] a, ref ushort[] b, ref ushort[] c)
        {
            int i;
            ushort u = 0;

            for (i = N; i >= 0; i--)
            {
                u += (ushort)(a[i] + b[i]);
                c[i] = (ushort)(u & (RADIX - 1));
                u >>= RADIXBITS;
            }
        }
    }
}