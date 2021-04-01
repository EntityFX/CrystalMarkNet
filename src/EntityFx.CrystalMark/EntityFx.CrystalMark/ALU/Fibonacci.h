#include "../CrystalBenchmarkBase.h"

using namespace std;

class Fibonacci : public CrystalBenchmarkBase
{
protected:
	virtual int	benchImplementation(const std::atomic_bool& cancelled) override;
};