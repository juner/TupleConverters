using System;
using BenchmarkDotNet.Attributes;

namespace TupleConverters.Extensions.ValuetupleExtensions.Benchmarks
{
    public class ValueTupleExtensionsBenchmark
    {
        [Benchmark]
        public void GetEnumerableTest1()
        {
            var num = 0;
            foreach (var i in ValueTuple.Create(1).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest1()
        {
            var num = 0;
            foreach (var i in ValueTuple.Create(1))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest2()
        {
            var num = 0;
            foreach (var i in (1, 2).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest2()
        {
            var num = 0;
            foreach (var i in (1, 2))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest3()
        {
            var num = 0;
            foreach (var i in (1, 2, 3).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest3()
        {
            var num = 0;
            foreach (var i in (1, 2, 3))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest4()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest4()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest5()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest5()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest6()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest6()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6))
                num += i;
        }

        [Benchmark]
        public void GetEnumerableTest7()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest7()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7))
                num += i;
        }
        [Benchmark]
        public void GetEnumerableTest8()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest8()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8))
                num += i;
        }
        [Benchmark]
        public void GetEnumerableTest15()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15).GetEnumerable())
                num += i;
        }

        [Benchmark]
        public void GetEnumeratorTest15()
        {
            var num = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15))
                num += i;
        }
    }
}
