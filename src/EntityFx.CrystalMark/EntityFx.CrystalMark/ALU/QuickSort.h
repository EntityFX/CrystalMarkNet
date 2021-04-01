#include "../CrystalBenchmarkBase.h"

using namespace std;

class QuickSort : public CrystalBenchmarkBase
{
	static const int MemSize = 2048;
protected:
	virtual int	benchImplementation(const std::atomic_bool& cancelled) override;
	static int compare(const void* arg1, const void* arg2);
};