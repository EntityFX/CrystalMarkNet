#include "FFT.h"
#include <iostream>
#include <cmath> 

int FFT::bench_implementation(const std::atomic_bool& cancelled) {
	unsigned short count = 0;
	const float PI = 3.14159265358979f;

	int i, j, k;

	float* fr = new float[N];
	float* fi = new float[N];

	float w, wr, wi, x, xr, xi;



	for (; ; )
	{
		for (i = 0; i < N; i++)
		{
			x = -2 * PI + (float)i * 4.0f * PI / (float)N;
			fr[i] = (float)(sin(x) + sin(2.0f * x));
			fi[i] = 0.0;
		}

		bitreverse(fr, N);
		bitreverse(fi, N);
		for (k = 1; k < N; k = k * 2)
		{
			for (j = 0; j < k; j++)
			{
				w = 2.0f * (float)PI / (2.0f * (float)k);
				wr = (float)cos(w * j);
				wi = (float)-sin(w * j);
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

		if (cancelled)
		{
			delete [] fr;
			delete [] fi;

			return (int)(count * (N / 1470.0 * 2));
		}
	}
	return count;
}

void FFT::bitreverse(float a[], int N) {
	int i, j, k, l;
	float* b = new float[N];

	for (i = 0; i < N; i++)
	{
		k = 0;
		l = N / 2;
		for (j = 1; j < N; j *= 2)
		{
			if (i & j) k += l;
			l /= 2;
		}
		b[i] = a[k];
	}
	for (i = 0; i < N; i++)
		a[i] = b[i];
	delete [] b;
}

string FFT::get_name() {
	return "FFT";
}