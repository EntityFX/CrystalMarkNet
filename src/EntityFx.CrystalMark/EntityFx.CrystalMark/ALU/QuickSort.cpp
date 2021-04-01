#include "QuickSort.h"
#include <iostream>
#include <cstring>

int QuickSort::bench_implementation(const std::atomic_bool& cancelled) {
    long long count = 0;
    unsigned int* memory = new unsigned int[MemSize / 4];
    unsigned int* test = new unsigned int[MemSize / 4];

    int temp = MemSize / 4;

    unsigned int holdrand = 1;
    for (int i = 0; i < temp; i++) {
        holdrand = ((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;

    }

    for (; ; )
    {
        std::memcpy(test, memory, MemSize);
        qsort((void*)test, MemSize / 4, sizeof(unsigned int), compare);
        count++;

        if (cancelled)
        {
            return (int)((double)count / 4.7 / 150);
        }
    }
    return count;
}

int QuickSort::compare(const void* arg1, const void* arg2)
{
    if (*(unsigned int*)arg1 > *(unsigned int*)arg2) { return -1; }
    else if (*(unsigned int*)arg1 == *(unsigned int*)arg2) { return 0; }
    else { return 1; }
}

string QuickSort::get_name() {
    return "QuickSort";
}