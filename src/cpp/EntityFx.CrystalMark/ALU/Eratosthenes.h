#include "../CrystalBenchmarkBase.h"



using namespace std;

class Eratosthenes : public CrystalBenchmarkBase
{
	const int N = 1024;
public:
	string get_name() override;
protected:
	virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
};