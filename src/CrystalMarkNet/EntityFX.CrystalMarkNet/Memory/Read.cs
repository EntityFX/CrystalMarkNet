using System.Threading;

namespace EntityFX.CrystalMarkNet.Memory
{
    class Read : CrystalBenchmarkBase
    {
        private const int MemSize = 2048 * 1024;

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            uint[] memory = new uint[MemSize / 4];
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = (uint)(i * i + i);
            }


            uint count = 0;

            uint a1, a2, a3, a4, a5, a6, a7, a8;

            for (;;)
            {
                for (int i = memory.Length - 1; i >= 7; i -= 8)
                {
                    a8 = memory[i - 7];
                    a7 = memory[i - 6];
                    a6 = memory[i - 5];
                    a5 = memory[i - 4];
                    a4 = memory[i - 3];
                    a3 = memory[i - 2];
                    a2 = memory[i - 1];
                    a1 = memory[i];
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