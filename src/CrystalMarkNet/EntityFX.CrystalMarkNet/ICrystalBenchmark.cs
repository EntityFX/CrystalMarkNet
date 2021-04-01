namespace EntityFX.CrystalMarkNet
{
    interface ICrystalBenchmark
    {
        int Bench(int threads);

        string Name { get; }
    }
}