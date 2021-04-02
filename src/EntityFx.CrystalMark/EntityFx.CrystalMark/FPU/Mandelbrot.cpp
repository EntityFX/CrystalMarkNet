#include "Mandelbrot.h"
#include <iostream>

int Mandelbrot::bench_implementation(const std::atomic_bool& cancelled) {
	unsigned short num = 1;
	unsigned short count = 0;

	int x, y, k, color = 0;
	double dr, di;
	int boost = 0;
	ri z, z2, c;

	dr = (RE - RS) / Mandelbrot_XS;
	di = (IE - IS) / Mandelbrot_YS;

	int bitmap[Mandelbrot_XS][Mandelbrot_YS];

	for (; ; )
	{
		for (y = 0; y < Mandelbrot_YS; y++)
		{
			for (x = Mandelbrot_XS - 1; x >= 0; x--)
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
						bitmap[x][y] = color;
						break;
					}
					z = z2;
				}
			}

			count++;

			if (cancelled)
			{
				return (int)(count / 100000 / 30);
			}
		}
		return count;
	}
}

string Mandelbrot::get_name() {
	return "Mandelbrot";
}