#pragma once

#include <string>
#include <future>

using namespace std;

class ICrystalBenchmark
{
public:
	string Name;
	virtual ~ICrystalBenchmark() {}
	virtual int bench(int threads) = 0;
};

class CrystalBenchmarkBase : virtual public ICrystalBenchmark
{
public:
	virtual int bench(int threads);
protected:
	bool isLoopEnd = false;
	virtual int	benchImplementation(const std::atomic_bool& cancelled) = 0;
	int benchAll(int threads);
};