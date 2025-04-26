using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        Console.WriteLine("Начало выполнения (параллельная версия)");

        int n = 128000000;
        int taskCount = 128;

        int rangeSize = n / taskCount;
        Task<int>[] tasks = new Task<int>[taskCount];

        for (int i = 0; i < taskCount; i++)
        {
            int start = i * rangeSize + 1;
            int end = (i == taskCount - 1) ? n : (start + rangeSize - 1);

            tasks[i] = Task.Run(() => Task01.Class1.CountDivisible(start, end));
        }

        Task.WaitAll(tasks);

        int totalResult = 0;
        foreach (var task in tasks)
        {
            totalResult += task.Result;
        }

        Console.WriteLine("Окончание выполнения (параллельная версия)");
        stopwatch.Stop();

        Console.WriteLine($"Количество чисел, делящихся на 2, 17 и 18 без остатка в диапазоне от 1 до {n}: {totalResult}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} миллисекунд");
    }
}
