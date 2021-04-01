#pragma once

#include <string>
#include <future>

using namespace std;

class ICrystalBenchmark
{
public:
	virtual string get_name() = 0;
	virtual ~ICrystalBenchmark() {}
	virtual int bench(int threads) = 0;
};

class CrystalBenchmarkBase : virtual public ICrystalBenchmark
{
public:
	virtual int bench(int threads);
protected:
	bool isLoopEnd = false;
	virtual int	bench_implementation(const std::atomic_bool& cancelled) = 0;
	int bench_all(int threads);
};