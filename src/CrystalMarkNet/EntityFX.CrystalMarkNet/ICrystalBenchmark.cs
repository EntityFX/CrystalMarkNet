namespace EntityFX.CrystalMarkNet
{
    interface ICrystalBenchmark
    {
        double Bench(int threads, int benchTime = 10000);

        string Name { get; }
    }
}