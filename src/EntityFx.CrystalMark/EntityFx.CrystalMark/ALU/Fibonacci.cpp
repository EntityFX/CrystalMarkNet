#include "Fibonacci.h"
#include <iostream>

int Fibonacci::benchImplementation(const std::atomic_bool& cancelled) {
    long long count = 0;
    int n = 0;

    int a, a1, b, b1, c, c1, x, x1, y, y1;
    for (; ; )
    {
        n = count + 2;
        a = 1;
        b = 1;
        c = 0;
        x = 1;
        y = 0;
        n--;
        while (n > 0)
        {
            if ((n & 1) == 1)
            {
                x1 = x;
                y1 = y;
                x = a * x1 + b * y1;
                y = b * x1 + c * y1;
            }

            n /= 2;
            a1 = a;
            b1 = b;
            c1 = c;
            a = a1 * a1 + b1 * b1;
            b = b1 * (a1 + c1);
            c = b1 * b1 + c1 * c1;
        }

        count++;

        if (cancelled)
        {
            return count / 2200 / 2000;
        }
    }
	return count;
}