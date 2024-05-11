using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EntityFX.CrystalMarkNet
{
    class CrystalBenchmarkGroup : CrystalBenchmarkBase
    {
        private readonly ICrystalBenchmark[] _crystalBenchmarks;

        public Dictionary<string, double> Results { get; } = new Dictionary<string, double>();

        public EventHandler<KeyValuePair<string, double>> OnResult { get; set; }


        public CrystalBenchmarkGroup(ICrystalBenchmark[] crystalBenchmarks, string groupName)
        {
            _crystalBenchmarks = crystalBenchmarks;
            Name = groupName;
        }

        public override string Name { get; }

        public override double Bench(int threads, int benchTime = DefaultTime)
        {
            foreach (var crystalBenchmark in _crystalBenchmarks)
            {
                Results[crystalBenchmark.Name] = crystalBenchmark.Bench(threads, benchTime);

                OnResult?.Invoke(this, 
                    new KeyValuePair<string, double>(crystalBenchmark.Name, Results[crystalBenchmark.Name]));
            }

            return Results.Values.Sum();
        }

        protected override double BenchImplementation(CancellationToken cancellationToken)
        {
            return 0;
        }
    }
}