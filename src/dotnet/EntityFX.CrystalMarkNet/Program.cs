using System;
using EntityFX.CrystalMarkNet.ALU;
using EntityFX.CrystalMarkNet.FPU;
using EntityFX.CrystalMarkNet.Memory;

namespace EntityFX.CrystalMarkNet
{
    class Program
    {


        static void Main(string[] args)
        {
            var threads = Environment.ProcessorCount;
            //threads = 1;

            if (args.Length > 0)
            {
                Int32.TryParse(args[0], out threads);
            }

            

            var memoryGroup = new CrystalBenchmarkGroup(new ICrystalBenchmark[]
            {
                 new Read(), new Write(), new ReadWrite(), 
            }, "Memory");


            var aluGroup = new CrystalBenchmarkGroup(new ICrystalBenchmark[]
            {
                 new Fibonacci(), new Napierian(), new Eratosthenes(), new QuickSort()
            }, "ALU");


            var fpuGroup = new CrystalBenchmarkGroup(new ICrystalBenchmark[]
            {
                 new MikoFpu(), new RandMeanSS(), new FFT(), new Mandelbrot()
            }, "FPU");

            var groups = new CrystalBenchmarkGroup[] { aluGroup, fpuGroup, memoryGroup };
            //var groups = new CrystalBenchmarkGroup[] { fpuGroup };

            foreach (var benchmarkGroup in groups)
            {
                Console.WriteLine("{0,-17}", string.Format("[ {0} ]", benchmarkGroup.Name));
                //Console.WriteLine("{0,-17}{1,8}", string.Format("[ {0} ]", benchmarkGroup.Name), result);
                benchmarkGroup.OnResult = (sender, pair) =>
                {
                    Console.WriteLine("{0,16}:{1,8:#}", pair.Key, pair.Value);
                };

                var result = benchmarkGroup.Bench(threads);


                //foreach (var groupResult in benchmarkGroup.Results)
                //{
                //    Console.WriteLine("{0,16}:{1,8}", groupResult.Key, groupResult.Value);
                //}

            }
        }





    }
}
