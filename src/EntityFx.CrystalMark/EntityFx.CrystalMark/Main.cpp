// EntityFx.CrystalMark.cpp : Defines the entry point for the application.
//

#include "Main.h"
#include "ALU/Fibonacci.h"
#include "ALU/Napierian.h"
#include "ALU/Eratosthenes.h"
#include "ALU/QuickSort.h"
#include "CrystalBenchmarkBase.h"

using namespace std;
using namespace std::chrono;

int main()
{
	int threads = std::thread::hardware_concurrency();

	vector<ICrystalBenchmark*> benchs{ new Fibonacci(), new Napierian(), new Eratosthenes(), new QuickSort() };

	int aluSum = 0;
	cout << "[ALU]" << "\n";
	for (auto& bench : benchs) // access by reference to avoid copying
	{
		int result = bench->bench(threads);
		cout << string_sprintf("%-12s: %6d", bench->get_name().c_str(), result) << "\n";
		aluSum += result;
	}

	cout << string_sprintf("%-12s: %6d", "[ALU]", aluSum) << "\n";

	return 0;
}
