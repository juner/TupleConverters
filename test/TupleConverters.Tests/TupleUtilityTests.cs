using TupleConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static TupleConverters.TupleUtility;
using System.Linq;

namespace TupleConverters.Tests
{
    [TestClass()]
    public class TupleUtilityTests
    {
        static IEnumerable<object?[]> ToTestData
        {
            get
            {
                // ValueTuple pattern 1-7
                yield return ToTest(typeof((int, string)), new object?[] { 1, "2" }, (1, "2"));
                // nested ValueTuple pattern 8 -
                yield return ToTest(typeof((int, string, int, string, int, string, int, string, int, string)), new object?[] { 1, "2", 3, "4", 5, "6", 7, "8", 9, "10" }, (1, "2", 3, "4", 5, "6", 7, "8", 9, "10"));
                // Tuple pattern 1-7
                yield return ToTest(typeof(Tuple<int, string>), new object?[] { 1, "2" }, Tuple.Create(1, "2"));
                // nested Tuple pattern 8 -
                yield return ToTest(typeof(Tuple<int, string, int, string, int, string, int, Tuple<string, int, string>>), new object?[] { 1, "2", 3, "4", 5, "6", 7, "8", 9, "10" }, new Tuple<int, string, int, string, int, string, int, Tuple<string, int, string>>(1, "2", 3, "4", 5, "6", 7, new Tuple<string, int, string>("8", 9, "10")));
                static object?[] ToTest(Type TupleType, IEnumerable<object?> Values, object Expected)
                    => new object?[] { TupleType, Values, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(ToTestData))]
        public void ToTest(Type TupleType, IEnumerable<object?> Values, object Expected)
             => Assert.AreEqual(Expected, To(TupleType, Values));
        static IEnumerable<object?[]> MakeTupleTypeTestData
        {
            get
            {
                yield return MakeTupleTypeTest(new Type[] { typeof(int), typeof(string) }, typeof(Tuple<int, string>));

                static object?[] MakeTupleTypeTest(IEnumerable<Type> Types, Type Expected)
                    => new object?[] { Types, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(MakeTupleTypeTestData))]
        public void MakeTupleTypeTest(IEnumerable<Type> Types, Type Expected)
            => Assert.AreEqual(Expected, MakeTupleType(Types));

        static IEnumerable<object?[]> MakeValueTupleTypeTestData
        {
            get
            {
                // ValueTuple pattern 1-7
                yield return MakeValueTupleTypeTest(new Type[] { typeof(int), typeof(string) }, typeof((int, string)));
                // nested ValueTuple pattern 8 -
                yield return MakeValueTupleTypeTest(new Type[] { typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string) }, typeof((int, string, int, string, int, string, int, string, int, string)));
                static object?[] MakeValueTupleTypeTest(IEnumerable<Type> Types, Type Expected)
                    => new object?[] { Types, Expected };
            }
        }

        [TestMethod]
        [DynamicData(nameof(MakeValueTupleTypeTestData))]
        public void MakeValueTupleTypeTest(IEnumerable<Type> Types, Type Expected)
            => Assert.AreEqual(Expected, MakeValueTupleType(Types));

        static IEnumerable<object?[]> IsTupleTestData
        {
            get
            {
                yield return IsTupleTest(typeof((int, string)), false);
                yield return IsTupleTest(typeof(Tuple<int, string>), true);
                static object?[] IsTupleTest(Type Type, bool Expected)
                    => new object?[] { Type, Expected };
            }
        }

        [TestMethod]
        [DynamicData(nameof(IsTupleTestData))]
        public void IsTupleTest(Type Type, bool Expected)
            => Assert.AreEqual(Expected, Type.IsTuple());

        static IEnumerable<object?[]> IsValueTupleTestData
        {
            get
            {
                yield return IsTupleTest(typeof((int, string)), true);
                yield return IsTupleTest(typeof(Tuple<int, string>), false);
                static object?[] IsTupleTest(Type Type, bool Expected)
                    => new object?[] { Type, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(IsValueTupleTestData))]
        public void IsValueTupleTest(Type Type, bool Expected)
            => Assert.AreEqual(Expected, Type.IsValueTuple());

        static IEnumerable<object?[]> FromTestData
        {
            get
            {
                yield return FromTest((1, "2"), new object?[] { 1, "2" });
                yield return FromTest(Tuple.Create(1, "2"), new object?[] { 1, "2" });
                yield return FromTest(1, new object?[0]);
                static object?[] FromTest(object? Source, object?[] Expected)
                    => new object?[] { Source, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(FromTestData))]
        public void FromTest(object? Source, object?[] Expected)
            => CollectionAssert.AreEqual(Expected, From(Source));
        public  static IEnumerable<object?[]> GetTypesTestData
        {
            get
            {
                yield return GetTypesTest(typeof(ValueTuple<int, string>), new[] { typeof(int), typeof(string) });
                yield return GetTypesTest(typeof(Tuple<int, string>), new[] { typeof(int), typeof(string) });
                yield return GetTypesTest(typeof(Tuple<int, string, int, string, int, string, int, Tuple<string, int, string>>), new[] { typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string) });
                yield return GetTypesTest(typeof(ValueTuple<int, string, int, string, int, string, int, ValueTuple<string, int, string>>), new[] { typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string), typeof(int), typeof(string) });
                yield return GetTypesTest(typeof(ValueTuple<int, string>), new[] { typeof(int), typeof(string) });
                static object?[] GetTypesTest(Type TupleType, Type[] Expected)
                    => new object?[] { TupleType, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetTypesTestData))]
        public void GetTypesTest(Type TupleType, Type[] Expected)
        {
            Console.WriteLine(nameof(Expected) + ":");
            foreach (var e in Expected)
                Console.WriteLine(e);
            var Actual = GetTypes(TupleType).ToArray();
            Console.WriteLine(nameof(Actual) + ":");
            foreach (var a in Actual)
                Console.WriteLine(a);
            CollectionAssert.AreEqual(Expected, Actual);
        }
    }
}