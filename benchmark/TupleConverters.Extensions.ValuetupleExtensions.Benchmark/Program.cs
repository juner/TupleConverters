using System;
using BenchmarkDotNet.Running;

namespace TupleConverters.Extensions.ValuetupleExtensions.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ValueTupleExtensionsBenchmark>();
        }
    }
}
