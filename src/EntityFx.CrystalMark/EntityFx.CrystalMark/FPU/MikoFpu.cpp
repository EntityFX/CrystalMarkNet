#include "MikoFpu.h"
#include <iostream>

int MikoFpu::bench_implementation(const std::atomic_bool& cancelled) {
    double count = 0;

    float a = 1.0f;
    float b = 0.0f;
    float c = 0.0f;
    float d = 0.0f;

    float xch = 0.0f;

    for (; ; )
    {
        b += a;
        a -= d;

        xch = c;
        c = a;
        a = xch;

        a += d;

        xch = c;
        c = a;
        a = xch;

        a *= b;

        count++;

        if (cancelled)
        {
            return (int)(count / 100000 / 30);
        }
    }
    return count;
}

string MikoFpu::get_name() {
    return "MikoFpu";
}