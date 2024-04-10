using System.Threading;

namespace EntityFX.CrystalMarkNet.FPU
{
    class Mandelbrot : CrystalBenchmarkBase
    {
        const int KL = 64;

        const double RS = -2.2 * 4 / 3;
        const double RE = 0.5 * 4 / 3;
        const double IS = -1.35;
        const double IE = 1.35;

        const int XS = 256;
        const int YS = 256;

        struct ri
        {
            public double r;
            public double i;
        }

        protected override double BenchImplementation(CancellationToken cancellationToken)
        {
            ushort count = 0;

            int x, y, k, color = 0;
            double dr, di;
            int boost = 0;
            ri z, z2, c;

            dr = (RE - RS) / XS;
            di = (IE - IS) / YS;

            for (; ; )
            {
                for (y = 0; y < YS; y++)
                {
                    for (x = XS - 1; x >= 0; x--)
                    {
                        c.r = x * dr + RS;
                        c.i = y * di + IS;
                        z.i = 0.0;
                        z.r = 0.0;
                        for (k = 0; k < KL; k++)
                        {
                            z2.r = z.r * z.r - z.i * z.i + c.r;
                            z2.i = 2.0 * z.r * z.i + c.i;
                            if (z2.r * z2.r + z2.i * z2.i > 4.0)
                            {
                                color = k * 8 << boost;
                                break;
                            }
                            z = z2;
                        }
                    }
                }

                count++;
                boost = count % 20;

                if (cancellationToken.IsCancellationRequested)
                {
                    return count / 0.012427d / 2;
                }
            }
        }
    }
}