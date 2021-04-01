#include "../CrystalBenchmarkBase.h"

using namespace std;

class Napierian : public CrystalBenchmarkBase
{
	static const int N = 225;
	static const int M = 250;
	static const unsigned short RADIXBITS = 15;

	static const unsigned short RADIX = (1 << RADIXBITS);

	static const unsigned short H_RADIX = RADIX / 2;
public:
	string get_name() override;
protected:
	virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
private:
	static int divs(int m, unsigned short a[], unsigned int x, unsigned short b[]);
	static void add(unsigned short a[], unsigned short b[], unsigned short c[]);
};