using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMP_Classes_Task3
{
    public class IntegerArray
    {
        private Dictionary<int, int> array;
        private Random random;

        const int MAX_RANDOM_VALUE = 100;
        const int MIN_RANDOM_VALUE = 1;

        public IntegerArray(int arrayLength)
        {
            random = new Random();

            array = new Dictionary<int, int>(arrayLength);
            for (int i = 0; i < arrayLength; i++)
                array.Add(i, 0);
        }

        public void InputData(int startIndex, params int[] values)
        {
            if (startIndex + values.Length > array.Count)
                throw new ArgumentOutOfRangeException();
            for (int i = startIndex; i < values.Length + startIndex; i++)
                array[i] = values[i - startIndex];
        }

        public void InputDataRandom()
        {
            for(int i = 0; i < array.Count; i++)
                array[i] = random.Next(MIN_RANDOM_VALUE,MAX_RANDOM_VALUE + 1);
        }

        public void Print(int startIndex, int endIndex)
        {
            try
            {
                var range = array.Values.ToList().GetRange(startIndex, endIndex - startIndex + 1);
                Console.WriteLine(String.Join(' ', range));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int[] FindValue(int value)
        {
            return array.Where(a => a.Value == value).Select(a => a.Key).ToArray();
        }

        public void DeleteValue(int value)
        {
            int[] indexes = FindValue(value);
            for(int i = 0; i < indexes.Length; i++)
            {
                for(int j = indexes[i]; j < array.Count - 1; j++)
                    array[j] = array[j + 1];
                for (int j = i + 1; j < indexes.Length; j++)
                    indexes[j]--;
            }
            for (int i = array.Count - 1; i >= array.Count - indexes.Length; i--)
                array[i] = 0;
        }

        public int FindMax() => array.Max(a => a.Value);

        public IntegerArray Add(in IntegerArray array2)
        {
            if (array.Count != array2.GetLength())
                throw new ArgumentException("Lengths of arrays must be equal");
            IntegerArray result = new IntegerArray(array.Count);
            for (int i = 0; i < array.Count; i++)
                result.array[i] = array[i] + array2.array[i];

            return result;
        }

        public IntegerArray Sort()
        {
            var newOrder = array.OrderBy(a => a.Value).ToArray();
            IntegerArray result = new IntegerArray(newOrder.Length);
            for (int i = 0; i < newOrder.Length; i++)
                result.array[i] = newOrder[i].Value;

            return result;
        }

        public int GetLength() => array.Count;

        public int this[int key]
        {
            get => array[key];
        }

        public override string ToString()
        {
            return String.Join(' ', array.Values);
        }
    }
}
