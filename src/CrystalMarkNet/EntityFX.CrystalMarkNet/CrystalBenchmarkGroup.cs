using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CrystalMarkNet
{
    class CrystalBenchmarkGroup : CrystalBenchmarkBase
    {
        private readonly ICrystalBenchmark[] _crystalBenchmarks;

        public Dictionary<string, int> Results { get; } = new Dictionary<string, int>(); 


        public CrystalBenchmarkGroup(ICrystalBenchmark[] crystalBenchmarks, string groupName)
        {
            _crystalBenchmarks = crystalBenchmarks;
            Name = groupName;
        }

        public override string Name { get; }

        public override int Bench(int threads)
        {
            foreach (var crystalBenchmark in _crystalBenchmarks)
            {
                Results[crystalBenchmark.Name] = crystalBenchmark.Bench(threads);
            }

            return Results.Values.Sum();
        }

        protected override int BenchImplementation(CancellationToken cancellationToken)
        {
            return 0;
        }
    }
}