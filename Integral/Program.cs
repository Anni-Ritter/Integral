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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            integral.Sol();
            watch.Stop();
            var elapsedMs = watch.Elapsed;
            Console.WriteLine("Время вычисления в многопоточном режиме: " + elapsedMs);
            Thread.Sleep(2000);
            Console.WriteLine("Интеграл 1/ln(x), промежуток от 2 до 5, число промежутков " + Environment.ProcessorCount.ToString());
            Console.WriteLine($"Результат: {integral.Result}");

            
            var result = CentralRectangle(x => 1 / Math.Log(x), 2, 5, 10);
            Console.WriteLine("Результат: {0}", result);
            
            Console.ReadKey();
        }
        
        static double CentralRectangle(Func<double, double> f, double a, double b, int n)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var h = (b - a) / n;
            var sum = (f(a) + f(b)) / 2;
            for (var i = 1; i < n; i++)
            {
                var x = a + h * i;
                sum += f(x);
            }

            var result = h * sum;
            
            var elapsedMs = watch.Elapsed;
            Console.WriteLine("Время вычисления в однопоточном режиме: " + elapsedMs);
            return result;
        }
    }
}
