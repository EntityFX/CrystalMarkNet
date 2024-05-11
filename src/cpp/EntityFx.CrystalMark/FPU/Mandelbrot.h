#include "../CrystalBenchmarkBase.h"



using namespace std;

struct ri
{
    double r;
    double i;
};

const int Mandelbrot_XS = 640;
const int Mandelbrot_YS = 480;

class Mandelbrot : public CrystalBenchmarkBase
{
    const int KL = 64;

    const double RS = -2.2 * 4 / 3;
    const double RE = 0.5 * 4 / 3;
    const double IS = -1.35;
    const double IE = 1.35;


public:
	string get_name() override;
protected:
	virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
};