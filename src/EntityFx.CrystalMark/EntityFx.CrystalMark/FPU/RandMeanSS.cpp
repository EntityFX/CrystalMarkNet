
#include "RandMeanSS.h"
#include <iostream>
#include <cmath> 

int RandMeanSS::bench_implementation(const std::atomic_bool& cancelled) {
    int count = 0;

    int i;
    double x, s1, s2;

    double* d = new double[N];
    static long holdrand = 1;

    for (; ; )
    {
		for (i = 0; i < N; i++) {
			holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;
			d[i] = (double)((1.0 / (RAND_MAX + 1.0)) * holdrand);
		}

		s1 = s2 = 0.0f;
		for (i = 1; i <= N; i++) {
			x = d[i - 1];
			x -= s1;
			s1 += x / i;
			s2 += (i - 1) * x * x / i;
		}
		s2 = sqrt(s2 / (N - 1));
		count++;

        if (cancelled)
        {
            delete[] d;

            return count / 90 / 80;
        }
    }

    return count;
}

string RandMeanSS::get_name() {
    return "RandMeanSS";
}