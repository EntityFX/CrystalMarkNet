#include "../CrystalBenchmarkBase.h"



using namespace std;

class Eratosthenes : public CrystalBenchmarkBase
{
	const int N = 1024;
protected:
	virtual int	benchImplementation(const std::atomic_bool& cancelled) override;
};