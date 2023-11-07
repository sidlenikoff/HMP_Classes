using HMP_Classes_Task3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace HMP_Classes_Tests
{
    [TestClass]
    public class Task3Tests
    {
        [TestMethod]
        public void TestTask3InitializeArray()
        {
            IntegerArray array = new IntegerArray(4);
            array.InputData(0, 1, 2, 3, 4);

            Assert.AreEqual("1 2 3 4", array.ToString());
        }

        [TestMethod]
        public void TestTask3InitializeRandomArray()
        {
            IntegerArray array = new IntegerArray(5);
            array.InputDataRandom();

            Assert.AreEqual(5, array.GetLength());

            for (int i = 0; i < array.GetLength(); i++)
                Assert.IsTrue(array[i] != 0);
        }

        [TestMethod]
        public void TestTask3FindValue()
        {
            IntegerArray array = new IntegerArray(10);
            array.InputData(0, 1, 2, 3, 4, 4, 5, 6, 7, 4, 9);

            var indexes = array.FindValue(4);

            var check = new int[] { 3, 4, 8 };

            Assert.AreEqual(check.Length, indexes.Length);
            for (int i = 0; i < indexes.Length; i++)
                Assert.AreEqual(check[i], indexes[i]);
        }

        [TestMethod]
        public void TestTask3DeleteValue()
        {
            IntegerArray array = new IntegerArray(10);
            array.InputData(0,1, 2, 3, 4, 4, 5, 6, 7, 4, 9);

            array.DeleteValue(4);

            Assert.AreEqual("1 2 3 5 6 7 9 0 0 0", array.ToString());
        }

        [TestMethod]
        public void TestTask3FindMax()
        {
            IntegerArray array = new IntegerArray(10);
            array.InputData(0, 1, 2, 3, 4, 4, 5, 6, 7, 4, 9);

            Assert.AreEqual(9, array.FindMax());
        }

        [TestMethod]
        public void TestTask3AddArrays()
        {
            IntegerArray array = new IntegerArray(10);
            array.InputData(0, 1, 2, 3, 4, 4, 5, 6, 7, 4, 9);

            IntegerArray array2 = new IntegerArray(10);
            array2.InputData(0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

            var result = array.Add(array2);

            Assert.AreEqual("3 5 7 9 10 12 14 16 14 20", result.ToString());
        }

        [TestMethod]
        public void TestTask3SortArray()
        {
            IntegerArray array = new IntegerArray(5);
            array.InputData(0, 100, -100, 4,32,0);

            var result = array.Sort();

            Assert.AreEqual("-100 0 4 32 100", result.ToString());
        }

    }
}
