// EntityFx.CrystalMark.cpp : Defines the entry point for the application.
//

#include "Main.h"
#include "ALU/Fibonacci.h"
#include "ALU/Napierian.h"
#include "ALU/Eratosthenes.h"
#include "ALU/QuickSort.h"
#include "FPU/MikoFpu.h"
#include "FPU/Mandelbrot.h"
#include "FPU/FFT.h"
#include "FPU/RandMeanSS.h"
#include "CrystalBenchmarkBase.h"

using namespace std;
using namespace std::chrono;

int main()
{
	int threads = std::thread::hardware_concurrency();

	vector<ICrystalBenchmark*> benchs{ new Fibonacci(), new Napierian(), new Eratosthenes(), new QuickSort() };

	vector<ICrystalBenchmark*> fpuBenchs{ new MikoFpu(), new RandMeanSS(), new FFT(), new Mandelbrot() };

	int aluSum = 0;
	cout << "[ALU]" << "\n";
	for (auto& bench : benchs) // access by reference to avoid copying
	{
		int result = bench->bench(threads);
		cout << string_sprintf("%-12s: %6d", bench->get_name().c_str(), result) << "\n";
		aluSum += result;
	}

	cout << string_sprintf("%-12s: %6d", "[ALU]", aluSum) << "\n";

	int fpuSum = 0;
	cout << "[FPU]" << "\n";
	for (auto& bench : fpuBenchs) // access by reference to avoid copying
	{
		int result = bench->bench(threads);
		cout << string_sprintf("%-12s: %6d", bench->get_name().c_str(), result) << "\n";
		fpuSum += result;
	}

	cout << string_sprintf("%-12s: %6d", "[FPU]", fpuSum) << "\n";
	cout << string_sprintf("%-12s: %6d", "Total", aluSum + fpuSum) << "\n";
	return 0;
}
