#include "../CrystalBenchmarkBase.h"



using namespace std;



class RandMeanSS : public CrystalBenchmarkBase
{
    const int N = 128;
public:
    string get_name() override;
protected:
    virtual int	bench_implementation(const std::atomic_bool& cancelled) override;
private:
    void bitreverse(float a[], int N);
};