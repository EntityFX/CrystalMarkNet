using System;
using CrystalMarkNet.ALU;
using CrystalMarkNet.FPU;

namespace CrystalMarkNet
{
    interface ICrystalBenchmark
    {
        int Bench(int threads);

        string Name { get; }
    }


    class Program
    {


        static void Main(string[] args)
        {
            var threads = Environment.ProcessorCount;

            if (args.Length > 0)
            {
                Int32.TryParse(args[0], out threads);
            }

            var aluGroup = new CrystalBenchmarkGroup(new ICrystalBenchmark[]
            {
                 new Fibonacci(), new Napierian(), new Eratosthenes(), new QuickSort()
            }, "ALU");


            var fpuGroup = new CrystalBenchmarkGroup(new ICrystalBenchmark[]
            {
                 new MikoFpu(), new RandMeanSS(), new FFT(), new Mandelbrot()
            }, "FPU");

            var groups = new CrystalBenchmarkGroup[] { aluGroup, fpuGroup };

            foreach (var benchmarkGroup in groups)
            {
                var result = benchmarkGroup.Bench(threads);

                Console.WriteLine("{0,-17}{1,8}", string.Format("[ {0} ]", benchmarkGroup.Name), result);
                foreach (var groupResult in benchmarkGroup.Results)
                {
                    Console.WriteLine("{0,16}:{1,8}", groupResult.Key, groupResult.Value);
                }

            }

            Console.ReadKey();
        }





    }
}
