using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CircularBuffer;

namespace CircularBuffer.UnitTests
{
    [TestClass]
    public class CircularBufferUnitTest
    {
        [TestMethod]
        public void AddLast()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddLast(i);
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddFirst()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Clear()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
                buffer.Reset();
                if (buffer.Size != 0)
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ToList()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
                List<Int32> bufferToList = buffer.ToList();
                if (bufferToList.Count == 0)
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ToArray()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
                Int32[] bufferToArray = buffer.ToArray();
                if (bufferToArray.Length == 0)
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Index()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
                Int32 item = buffer[2];
                if (item != buffer[2])
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Enumerator()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(10);
                for (Int32 i = 0; i < 10; i++)
                {
                    buffer.AddFirst(i);
                }
                foreach (var item in buffer)
                {

                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Dynamic()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(12);
                buffer.IsDynamic = true;
                for (Int32 i = 0; buffer.Size != buffer.Capacity; i++)
                {
                    buffer.AddLast(i);
                }
                buffer.AddLast(12);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Infinite()
        {
            try
            {
                CircularBuffer<Int32> buffer = new CircularBuffer<Int32>(12);
                buffer.IsInfinite = true;
                for (Int32 i = 0; buffer.Size != buffer.Capacity; i++)
                {
                    buffer.AddLast(i);
                }
                buffer.AddLast(12);
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Gets a Random Int32 Number
        /// </summary>
        private static Int32 GetRandomNumber()
        {
            return GetRandomNumber(0, 100);
        }

        /// <summary>
        /// Gets a Random Int32 Number between two values
        /// </summary>
        private static Int32 GetRandomNumber(Int32 minValue, Int32 maxValue)
        {
            Thread.Sleep(100);
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }
    }
}