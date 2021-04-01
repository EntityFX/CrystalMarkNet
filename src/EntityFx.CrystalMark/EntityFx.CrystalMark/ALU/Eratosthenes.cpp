#include "Eratosthenes.h"
#include <iostream>

int Eratosthenes::bench_implementation(const std::atomic_bool& cancelled) {
    long long count = 0;

    int i, p, k, cnt = 0;
    char* flag = new char[N + 1];

    for (; ; )
    {
        for (i = 0; i <= N; i++) { flag[i] = true; }
        for (i = 0; i <= N; i++) {
            if (flag[i]) {
                p = i + i + 3;
                for (k = i + p; k <= N; k += p) { flag[k] = false; }
                cnt++;
            }
        }
        count++;

        if (cancelled)
        {
            return (int)(count * (N / 70000.0 / 15));
        }
    }
    return count;
}

string Eratosthenes::get_name() {
    return "Eratosthenes";
}