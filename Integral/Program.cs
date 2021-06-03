using System;
using System.Threading;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {

            double[] result = new double[20];
            const double b = 5;
            const double a = 2;
            double[] arr = new double[20];
            Console.WriteLine("Интеграл 1/ln(x), промежуток от 2 до 5, число промежутков " + Environment.ProcessorCount.ToString());
            int num = Environment.ProcessorCount;
            double h = (b - a) / num;
            for (int i = 0; i <= num; i++)
            {
                arr[i] = a + i * h;
            }
            Thread[] t = new Thread[num + 1];
            for (int i = 1; i <= num; i++)
            {
                int x = i;
                Thread thr = new Thread(delegate () { Rect(arr[x - 1], arr[x], 0.0001, result, x); });
                t[i] = thr;
                t[i].Start();
            }
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 1; i <= num; i++)
            {
                t[i].Join();
                result[num + 1] += result[i];
            }
            Console.WriteLine("Результат: " + result[num + 1]);
            watch.Stop();
            var elapsedMs = watch.Elapsed;
            Console.WriteLine("Время вычисления: " + elapsedMs);
            Console.ReadKey();
        }
        static void Rect(double a, double b, double e, double[] sarr, int num)
        {
            int n = 2;
            double Jn, x, s;
            double Jn1 = 0;
            do
            {
                double h = (b - a) / n;
                x = a + h;
                s = 0;
                for (int i = 0; i <= (n - 1); i++)
                {
                    double f = Function(x);
                    s += f;
                    x += h;
                }
                Jn = s * h;
                if (Math.Abs(Jn1 - Jn) <= e)
                {
                    sarr[num] = Jn;
                    return;
                }
                else
                {
                    Jn1 = Jn;
                    n = 2 * n;
                }
            }
            while (true);
        }
        static double Function(double x)
        {
            double rez = 1 / Math.Log(x);
            return rez;
        }
    }
}
