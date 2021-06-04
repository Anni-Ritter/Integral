using System;
using System.Threading;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {
            var integral = new Solution(
                f: x => 1 / Math.Log(x),
                a: 2,
                b: 5,
                e: 0.001
            );
            integral.Sol();
            Thread.Sleep(2000);
            Console.WriteLine("Интеграл 1/ln(x), промежуток от 2 до 5, число промежутков " + Environment.ProcessorCount.ToString());
            Console.WriteLine($"Результат: {integral.Result}");
            Console.ReadKey();
        }
    }
}
