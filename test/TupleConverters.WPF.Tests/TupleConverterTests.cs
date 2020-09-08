using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace TupleConverters.WPF.Tests
{
    [TestClass]
    public class TupleConverterTests
    {
        static IEnumerable<object?[]> ConvertTestData
        {
            get
            {
                yield return ConvertTest(new TupleConverter(), new object?[] { 1, "2" }, typeof(string), null, CultureInfo.CurrentCulture, Tuple.Create(1, "2"));
                static object?[] ConvertTest(TupleConverter Converter, object?[] values, Type targetType, object? parameter, CultureInfo culture, object? Expected)
                    => new object?[] { Converter, values, targetType, parameter, culture, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(ConvertTestData))]
        public void ConvertTest(TupleConverter Converter, object?[] values, Type targetType, object? parameter, CultureInfo culture, object? Expected)
            => Assert.AreEqual(Expected, Converter.Convert(values, targetType, parameter, culture));

        static IEnumerable<object?[]> ConvertBackTestData
        {
            get
            {
                yield return ConvertBackTest(new TupleConverter(), Tuple.Create(1, "2"), new[] { typeof(int), typeof(string) }, null, CultureInfo.CurrentCulture, new object?[] { 1, "2" });
                static object?[] ConvertBackTest(TupleConverter Converter, object? value, Type[] targetTypes, object? parameter, CultureInfo culture, object?[] Expected)
                    => new object?[] { Converter, value, targetTypes, parameter, culture, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(ConvertBackTestData))]
        public void ConvertBackTest(TupleConverter Converter, object? value, Type[] targetTypes, object? parameter, CultureInfo culture, object?[] Expected)
            => CollectionAssert.AreEqual(Expected, Converter.ConvertBack(value, targetTypes, parameter, culture));
    }
}