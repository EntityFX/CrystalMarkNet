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

        const int XS = 640;
        const int YS = 480;

        struct ri
        {
            public double r;
            public double i;
        }

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            ushort num = 1;
            ushort count = 0;

            int x, y, k, color = 0;
            double dr, di;
            int boost = 0;
            ri z, z2, c;

            dr = (RE - RS) / XS;
            di = (IE - IS) / YS;

            int[,] bitmap = new int[XS,YS];

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
                                bitmap[x, y] = color;
                                break;
                            }
                            z = z2;
                        }
                    }
                }

                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return (int)(count * 2);
                }
            }
        }
    }
}