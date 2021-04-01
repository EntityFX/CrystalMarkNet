using System.Threading;

namespace EntityFX.CrystalMarkNet.Memory
{
    class Write : CrystalBenchmarkBase
    {
        private const int MemSize = 2048 * 1024;

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            uint[] memory = new uint[MemSize / 4];
            uint count = 0;

            uint a1 = 64, a2 = 32, a3 = 16, a4 = 8, a5 = 4, a6 = 2, a7 = 1, a8 = 0;

            for (; ; )
            {
                for (int i = memory.Length - 1; i >= 7; i -= 8)
                {
                     memory[i - 7] = a8;
                     memory[i - 6] = a7;
                     memory[i - 5] = a6;
                     memory[i - 4] = a5;
                     memory[i - 3] = a4;
                     memory[i - 2] = a3;
                     memory[i - 1] = a2;
                     memory[i] = a1;
                }

                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return (int)count * (MemSize / 1024) / 1024 / 10;
                }
            }


        }
    }
}