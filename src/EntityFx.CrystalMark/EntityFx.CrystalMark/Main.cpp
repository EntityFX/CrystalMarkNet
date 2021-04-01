// EntityFx.CrystalMark.cpp : Defines the entry point for the application.
//

#include "Main.h"
#include "ALU/Fibonacci.h"
#include "ALU/Napierian.h"
#include "ALU/Eratosthenes.h"
#include "ALU/QuickSort.h"
#include "CrystalBenchmarkBase.h"
#include <string>
#include <chrono>
#include <omp.h>

using namespace std;
using namespace std::chrono;

int main()
{
	ICrystalBenchmark * bench = new Fibonacci();
	int result = bench->bench(1);
	cout << result << "\n";

	ICrystalBenchmark* bench2 = new Napierian();
	int result2 = bench2->bench(1);

	cout << result2 << "\n";

	ICrystalBenchmark* bench3 = new Eratosthenes();
	int result3 = bench3->bench(1);

	cout << result3 << "\n";

	ICrystalBenchmark* bench4 = new QuickSort();
	int result4 = bench4->bench(1);

	cout << result4 << "\n";

	return 0;
}
