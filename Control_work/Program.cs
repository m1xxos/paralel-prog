using System;
using System.Diagnostics;

namespace Task01
{
    public static class Class1
    {
        public static int CountDivisible(int start, int end)
        {
            int count = 0;
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0 && i % 17 == 0 && i % 18 == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

class Program
{
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Console.WriteLine("Начало выполнения");
        int n = 12800000;
        int result = Task01.Class1.CountDivisible(1, n);

        Console.WriteLine("Окончание выполнения");
        stopwatch.Stop();

        Console.WriteLine($"Количество чисел, делящихся на 2, 17 и 18 без остатка в диапазоне от 1 до {n}: {result}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} миллисекунд");
    }
}
