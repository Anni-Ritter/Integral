using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Integral
{
    public class Solution
    {
        private Func<double, double> func;

        private double leftSide;
        private double rightSide;
        private double eps;

        private Semaphore semaphore;

        private double result;

        public double Result
        {
            get { return result; }
        }

        public Solution(Func<double, double> f, double a, double b, double e)
        {
            func = f;
            semaphore = new Semaphore(Environment.ProcessorCount, Environment.ProcessorCount);
            leftSide = a;
            rightSide = b;
            eps = e;  
        }

        private void Rect(object args)
        {
            semaphore.WaitOne();

            var left = (double)((object[])args)[0];
            var right = (double)((object[])args)[1];

            var distance = right - left;

            var h = distance / 2;
            var center = left + h;

            if (distance < eps)
            {
                result += func(center) * distance;
            }
            else
            {
                var threadLeft = new Thread(Rect);
                threadLeft.Start(new object[] { left, center });

                var threadRight = new Thread(Rect);
                threadRight.Start(new object[] { center, right });
            }

            semaphore.Release();
        }
        public void Sol()
        {
            var thread = new Thread(Rect);
            thread.Start(new object[] { leftSide, rightSide });
        }
    }
}
