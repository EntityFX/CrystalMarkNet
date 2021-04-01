using System.Threading;

namespace EntityFX.CrystalMarkNet.Memory
{
    class ReadWrite : CrystalBenchmarkBase
    {
        private const int MemSize = 2048 * 1024;

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            uint[] memory = new uint[MemSize / 4];
            uint[] memory2 = new uint[MemSize / 4];
            uint count = 0;

            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = (uint)(i * i + i);
            }

            uint a1 = 64, a2 = 32, a3 = 16, a4 = 8, a5 = 4, a6 = 2, a7 = 1, a8 = 0;

            for (; ; )
            {
                for (int i = memory.Length - 1; i >= 7; i -= 8)
                {
                    memory2[i - 7] = memory[i - 7] + a8;
                    memory2[i - 6] = memory[i - 6] + a7;
                    memory2[i - 5] = memory[i - 5] + a6;
                    memory2[i - 4] = memory[i - 4] + a5;
                    memory2[i - 3] = memory[i - 3] + a4;
                    memory2[i - 2] = memory[i - 2] + a3;
                    memory2[i - 1] = memory[i - 1] + a2;
                    memory2[i] = memory[i - 1] + a1;
                }

                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return (int)count * 2 * (MemSize / 1024) / 1024 / 10;
                }
            }


        }
    }
}