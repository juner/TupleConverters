using Microsoft.VisualStudio.TestTools.UnitTesting;
using TupleConverters.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TupleConverters.Extensions.Tests
{
    [TestClass()]
    public class ValueTupleExtensionsTests
    {
        [TestMethod()]
        public void GetEnumerableTest1()
        {
            var expected = 0;
            foreach (var i in ValueTuple.Create(1).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(1, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest1()
        {
            var expected = 0;
            foreach (var i in ValueTuple.Create(1))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(1, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest2()
        {
            var expected = 0;
            foreach (var i in (1, 2).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(2, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest2()
        {
            var expected = 0;
            foreach (var i in (1, 2))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(2, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest3()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(3, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest3()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(3, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest4()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(4, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest4()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(4, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest5()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(5, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest5()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(5, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest6()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(6, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest6()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(6, expected);
        }

        [TestMethod()]
        public void GetEnumerableTest7()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(7, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest7()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(7, expected);
        }
        [TestMethod()]
        public void GetEnumerableTest8()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(8, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest8()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(8, expected);
        }
        [TestMethod()]
        public void GetEnumerableTest15()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15).GetEnumerable())
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(15, expected);
        }

        [TestMethod()]
        public void GetEnumeratorTest15()
        {
            var expected = 0;
            foreach (var i in (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15))
                Assert.AreEqual(++expected, i);
            Assert.AreEqual(15, expected);
        }
    }
}