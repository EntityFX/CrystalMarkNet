using System;
using System.Threading;

namespace EntityFX.CrystalMarkNet.ALU
{
    class QuickSort : CrystalBenchmarkBase
    {
        private const int MemSize = 2048;

        protected override double BenchImplementation(CancellationToken cancellationToken)
        {
            uint[] memory = new uint[MemSize / 4];
            uint[] test = new uint[MemSize / 4];
            uint count = 0;
            int temp = MemSize / 4;

            uint holdrand = 1;
            for (int i = 0; i < temp; i++)
            {
                holdrand = (uint)((holdrand * 214013L + 2531011L) >> 16) & 0x7fff;

                memory[i] = holdrand;
            }

            for (; ; )
            {
                for (int i = 0; i < temp; i += 4)
                {
                    test[i] = memory[i];
                    test[i + 1] = memory[i + 1];
                    test[i + 2] = memory[i + 2];
                    test[i + 3] = memory[i + 3];
                }
                //Array.Copy(memory, test, temp);
                Array.Sort(test);
                count++;

                if (cancellationToken.IsCancellationRequested)
                {
                    return count / 4.7d / 5.0d;
                }
            }
        }
    }
}