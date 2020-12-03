using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5
{
    class Program
    {
        static double dt = 0.000000001;

        static double X(double t)
        {
            return (2 * t) - (Math.Pow(t, 2) * 5 / 3) + (Math.Pow(t, 3) * 3 / 7);
        }

        static double F(double t)
        {
            return (X(t + dt) - X(t - dt)) / (2 * dt);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("v = " + F(0));
            Console.WriteLine();
            double t = 0;
            double e = 0.1;
            Console.WriteLine("Enter order of precision");
            int accuracy = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("t x F':");
            while (accuracy > 0)
            {
                if (F(t + e) < 0)
                {
                    e /= 10;
                    accuracy--;
                }
                Console.WriteLine("t = " + t + "\t" + "x = " + X(t) + "\t" + "F' = " + F(t));
                t += e;
            }
            t -= e;
            Console.WriteLine();
            Console.WriteLine("Distance(X(t) --> v~0):");
            Console.WriteLine(X(t));
            Console.ReadKey();
        }
    }
}
