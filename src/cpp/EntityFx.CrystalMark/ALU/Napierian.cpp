#include "Napierian.h"
#include <iostream>

int Napierian::bench_implementation(const std::atomic_bool& cancelled) {
    long long count = 0;
    unsigned short* a = new unsigned short[N + 1];
    unsigned short* t = new unsigned short[N + 1];

    for (; ; )
    {
        int m;
        unsigned int k;
        for (m = 0; m <= N; m++) {
            a[m] = t[m] = 0;
        }
        a[0] = 2;
        a[1] = t[1] = RADIX / 2;
        k = 3; m = 1;
        while ((m = divs(m, t, k, t)) <= N) {
            add(a, t, a);
            ++k;
        }

        count++;

        if (cancelled)
        {
            return (int)(count / 3.4 );
        }
    }
    return count;
}

int Napierian::divs(int m, unsigned short a[], unsigned int x, unsigned short b[])
{
    int i;
    unsigned long long t = 0;

    for (i = m; i <= N; i++)
    {
        t = (t << RADIXBITS) + a[i];
        b[i] = (unsigned short)(t / x);
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

void Napierian::add(unsigned short a[], unsigned short b[], unsigned short c[])
{
    int i;
    unsigned short u = 0;

    for (i = N; i >= 0; i -= 2)
    {
        u += a[i] + b[i];
        c[i] = u & (RADIX - 1);
        u >>= RADIXBITS;

        u += a[i - 1] + b[i - 1];
        c[i - 1] = u & (RADIX - 1);
        u >>= RADIXBITS;
    }
}

string Napierian::get_name() {
    return "Napierian";
}