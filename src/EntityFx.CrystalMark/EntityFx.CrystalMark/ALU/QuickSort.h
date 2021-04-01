#include "../CrystalBenchmarkBase.h"

using namespace std;

class QuickSort : public CrystalBenchmarkBase
{
	static const int MemSize = 2048;
public:
	string get_name() override;
protected:
	virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
	static int compare(const void* arg1, const void* arg2);
};