using HMP_Classes_Task1;

namespace HMP_Classes_Tests
{
    [TestClass]
    public class ClassesTest
    {
        [TestMethod]
        public void TestTask1CorrectInput()
        {
            string[] input = { "12 3 2 68 2740" };
            var result = Program.ParseInputToInts(input);
            Assert.AreEqual(1, result.Length);
            int[] check1 = new int[] { 12, 3, 2, 68, 2740 };
            var intersect1 = result[0].Intersect(check1);
            Assert.IsTrue(intersect1.Count() == result[0].Length && result[0].Length == check1.Length);

        }

        [TestMethod]
        public void TestTask1ExtraSpaces()
        {
            string[] input = { "12 3 2 68      2740" };
            var result = Program.ParseInputToInts(input);
            Assert.AreEqual(1, result.Length);
            int[] check1 = new int[] { 12, 3, 2, 68, 2740 };
            var intersect1 = result[0].Intersect(check1);
            Assert.IsTrue(intersect1.Count() == result[0].Length && result[0].Length == check1.Length);
        }

        [TestMethod]
        public void TestTask1NotIntegers()
        {
            string[] input = { "12 3 ovir 68 botrinw 9.2" };
            var result = Program.ParseInputToInts(input);
            Assert.AreEqual(1, result.Length);
            int[] check1 = new int[] { 12, 3, 0, 68, 0, 0 };
            for (int i = 0; i < check1.Length; i++)
                Assert.AreEqual(check1[i], result[0][i]);
        }

        [TestMethod]
        public void TestTask1MultipleLineInput()
        {
            string[] input = { "12 3 ovir 68 botrinw 9.2", "7 5 3 1", "    0 73   326 85" };
            var result = Program.ParseInputToInts(input);
            int[][] check1 = new int[][] { 
                new int[]{ 12, 3, 0, 68, 0, 0 } ,
            new int[]{ 7,5,3,1},
            new int[] { 0, 73, 326, 85} };
            Assert.AreEqual(check1.Length, result.Length);
            for (int i = 0; i < check1.Length; i++)
                for(int j = 0; j < check1[i].Length; j++)
                    Assert.AreEqual(check1[i][j], result[i][j]);
        }

        [TestMethod]
        public void TestTask1FindMinForEachLine()
        {
            string[] input = { "12 3 68 ", "7 5 3 1", "    0 73   326 85" };
            var result = Program.ParseInputToInts(input);
            int[] check = { 3, 1, 0 };

            var minInLines = Program.FindMinInEachLine(result);
            for (int i = 0; i < minInLines.Length; i++)
                Assert.AreEqual(check[i], minInLines[i]);
        }

        [TestMethod]
        public void TestTask1FindMaxForEachLine()
        {
            string[] input = { "12 3 68 bontr ontreo 0.32 nuv ", "7 5 3 1", "    0 73   326 85" };
            var result = Program.ParseInputToInts(input);
            int[] check = { 68, 7, 326 };

            var maxInLines = Program.FindMaxInEachLine(result);
            for (int i = 0; i < maxInLines.Length; i++)
                Assert.AreEqual(check[i], maxInLines[i]);
        }

        [TestMethod]
        public void TestTask1FindSumForEachLine()
        {
            string[] input = { "12 3 68 bontr ontreo 0.32 nuv ", "7 5 3 1", "    0 73   326 85" };
            var result = Program.ParseInputToInts(input);
            int[] check = { 83, 16, 484 };

            var sumInLines = Program.FindSumInEachLine(result);
            for (int i = 0; i < sumInLines.Length; i++)
                Assert.AreEqual(check[i], sumInLines[i]);
        }


    }
}