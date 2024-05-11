#include "../CrystalBenchmarkBase.h"



using namespace std;

class MikoFpu : public CrystalBenchmarkBase
{
public:
	string get_name() override;
protected:
	virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
};