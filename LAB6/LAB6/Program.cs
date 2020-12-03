using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6
{
    class Program
    {
        static double F(double t, double y)
        {
            return 2 * t * Math.Pow(y, 2);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("-=Euler's method=-");
            double h = 0.1;
            double t0 = 0;
            double y0 = 1;
            Console.WriteLine();
            Console.WriteLine("f(t, y) = y' = 2t + y^2");
            Console.WriteLine("y(0) = 1");
            Console.WriteLine("[0, 0.9]");
            Console.WriteLine("h = 0.1");
            Console.WriteLine();
            Console.WriteLine("i\tt\ty\tf(xi, yi)\thf(xi, yi)");
            double hfi = F(t0, y0) * h;
            double ti = t0;
            double yi = y0;
            for (int i = 0; ti <= 0.9; i++)
            {
                Console.WriteLine($"{i}\t{ti}\t{yi}\t{F(ti, yi)}\t{hfi}");
                ti += h;
                yi += hfi;
                hfi = F(ti, yi) * h;
            }
            Console.ReadKey();
        }
    }
}
