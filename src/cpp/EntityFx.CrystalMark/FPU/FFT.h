#include "../CrystalBenchmarkBase.h"



using namespace std;



class FFT : public CrystalBenchmarkBase
{
    const int N = 128;
    const float PI = 3.14159265358979f;
public:
    string get_name() override;
protected:
    virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
private:
    void bitreverse(float a[], int N);
};