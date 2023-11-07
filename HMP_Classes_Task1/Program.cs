namespace HMP_Classes_Task1
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool needBreak = false;
            while (!needBreak)
            {
                Console.WriteLine("\nInput N: ");
                try
                {
                    int N = Convert.ToInt32(Console.ReadLine());
                    string[] input = new string[N];
                    for (int i = 0; i < N; i++)
                        input[i] = Console.ReadLine();

                    var result = ParseInputToInts(input);
                    var sumForLines = FindSumInEachLine(result);
                    var minForLines = FindMinInEachLine(result);
                    var maxForLines = FindMaxInEachLine(result);
                    Console.WriteLine("\nРезультат выполнения программы:");
                    for (int i = 0; i < result.Length; i++)
                        Console.WriteLine($"{String.Join(' ', result[i])}\nMax = {maxForLines[i]}\n" +
                            $"Min = {minForLines[i]}\nSum = {sumForLines[i]}\n");

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Oops... Something went wrong\n{ex.Message}");
                }
                Console.WriteLine("\nClick 'y' to continue, other button to cancel");
                var info = Console.ReadKey();
                if (info.Key != ConsoleKey.Y)
                    needBreak = true;
            }
        }

        static public int[][] ParseInputToInts(in string[] input)
        {
            int[][] resultInts = new int[input.Length][];
            int index = 0;
            foreach(var s in input)
            {
                var splitted = s.Split(' ').ToArray();
                List<int> newRow = new List<int>();
                foreach(var it in splitted)
                {
                    if (String.IsNullOrEmpty(it))
                        continue;
                    int result;
                    bool isParsed = int.TryParse(it, out result);
                    if (isParsed)
                        newRow.Add(result);
                    else
                        newRow.Add(0);
                }
                resultInts[index] = newRow.ToArray();
                index++;
            }

            return resultInts;
        }

        static public int[] FindMaxInEachLine(in int[][] lines)
        {
            int[] result = new int[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                int maxInLine = int.MinValue;
                for (int j = 0; j < lines[i].Length; j++)
                    maxInLine = Math.Max(lines[i][j], maxInLine);
                result[i] = maxInLine;
            }

            return result;
        }

        static public int[] FindMinInEachLine(in int[][] lines)
        {
            int[] result = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                int minInLine = int.MaxValue;
                for (int j = 0; j < lines[i].Length; j++)
                    minInLine = Math.Min(lines[i][j], minInLine);
                result[i] = minInLine;
            }

            return result;
        }

        static public int[] FindSumInEachLine(in int[][] lines)
        {
            int[] result = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    result[i] += lines[i][j];

            return result;
        }
    }
}