using System;
using System.Diagnostics;
using System.Threading;
using Thread01;

class Program
{
    static void RunTest(int runNumber)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Class1.ClearResult();
        Console.WriteLine($"Запуск {runNumber}: Start");

        Thread t1 = new Thread(Class1.Op1);
        Thread t2 = new Thread(Class1.Op1);
        Thread t3 = new Thread(Class1.Op1);
        Thread t4 = new Thread(Class1.Op1);
        Thread t5 = new Thread(Class1.Op1);

        t1.Start(new Params() { A = 0, B = 1 });
        t2.Start(new Params() { A = 2, B = 3 });
        t3.Start(new Params() { A = 4, B = 5 });
        t4.Start(new Params() { A = 6, B = 7 });
        t5.Start(new Params() { A = 8, B = 9 });

        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
        t5.Join();

        stopwatch.Stop();
        Console.WriteLine($"Запуск {runNumber}: Stop");
        Console.WriteLine("Sum = " + Class1.GetResult());
        Console.WriteLine("Время: " + stopwatch.Elapsed.TotalMilliseconds + " мс");
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        for (int i = 1; i <= 3; i++)
        {
            RunTest(i);
        }
    }
}
