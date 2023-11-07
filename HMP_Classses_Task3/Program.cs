using System;
using System.Text;

namespace HMP_Classes_Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество элементов в массиве: ");
            int N = Convert.ToInt32(Console.ReadLine());
            IntegerArray array = new IntegerArray(N);
            RunArrayInputMethodMenu(ref array);
            RunArrayOperationMenu(ref array);
        }

        static void RunArrayOperationMenu(ref IntegerArray array)
        {
            bool needBreak = false;
            while(!needBreak)
            {
                ShowOperationsMenu();
                var operation = Console.ReadLine();
                switch(operation)
                {
                    case "1":
                        Console.WriteLine("Введите начальный и конечный индексы для вывода: ");
                        var inp = Console.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();
                        array.Print(inp[0], inp[1]);
                        break;
                    case "2":
                        Console.Write("Введите значение для поиска: ");
                        int valueToFind = Convert.ToInt32(Console.ReadLine());
                        var valueIndexes = array.FindValue(valueToFind);
                        Console.WriteLine("Данное значение находится в массиве под индексами:");
                        Console.WriteLine(String.Join(' ', valueIndexes));
                        break;
                    case "3":
                        Console.Write("Введите значение для удаления: ");
                        int valueToDelete = Convert.ToInt32(Console.ReadLine());
                        array.DeleteValue(valueToDelete);
                        Console.WriteLine("Массив после удаления введенного значения:");
                        Console.WriteLine(array.ToString());
                        break;
                    case "4":
                        Console.WriteLine($"Максимальное значение в массиве: {array.FindMax()}");
                        break;
                    case "5":
                        {
                            Console.Write("Введите количество элементов во втором массиве: ");
                            int newArrayLength = Convert.ToInt32(Console.ReadLine());
                            IntegerArray newArray = new IntegerArray(newArrayLength);
                            RunArrayInputMethodMenu(ref newArray);
                            try
                            {
                                var resultArray = array.Add(newArray);
                                Console.WriteLine("Результат сложения массивов:");
                                Console.WriteLine(resultArray.ToString());
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        }
                    case "6":
                        var sortedArray = array.Sort();
                        Console.WriteLine("Отсоритированный массив:");
                        Console.WriteLine(sortedArray.ToString());
                        break;
                    case "0":
                        needBreak = true;
                        break;
                    default:
                        Console.WriteLine("Выбранной комманды не сущетсвует");
                        break;
                }

                if (!needBreak)
                {
                    Console.WriteLine("Нажмите любую кнопку для продолжения работы...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowOperationsMenu()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Print()");
            Console.WriteLine("2. FindValue()");
            Console.WriteLine("3. DeleteValue()");
            Console.WriteLine("4. FindMax()");
            Console.WriteLine("5. Add()");
            Console.WriteLine("6. Sort()");
            Console.WriteLine("0. Выход");
            Console.Write("> ");
        }

        static void RunArrayInputMethodMenu(ref IntegerArray array)
        {
            bool needBreak = false;
            while (!needBreak)
            {
                Console.WriteLine("1. Ввести значения массива вручную");
                Console.WriteLine("2. Заполнить массив случайными значениями");
                Console.WriteLine("0. Выход");
                Console.Write("> ");
                var inputMethod = Console.ReadLine();
                switch (inputMethod)
                {
                    case "1":
                        Console.WriteLine("Введите значения массива через пробел: ");
                        var input = Console.ReadLine().Split(' ').Select(it => Convert.ToInt32(it)).ToArray();
                        array.InputData(0, input);
                        needBreak = true;
                        break;
                    case "2":
                        array.InputDataRandom();
                        Console.WriteLine("Массив заполнен случайными значениями");
                        needBreak = true;
                        break;
                    case "0":
                        needBreak = true;
                        break;
                    default:
                        Console.WriteLine("Такого варианта не существует");
                        break;
                }

                if (needBreak)
                {
                    Console.WriteLine("Введен массив:");
                    Console.WriteLine(array.ToString());
                }
            }
        }
    }
}