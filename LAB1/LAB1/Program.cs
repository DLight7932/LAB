using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLibrary;

namespace LAB1
{
    class Program
    {
        static double x1 = 0;
        static double x2 = 10;
        static double dx = 0.005;

        static double M(double x)
        {
            double result = 0.00000001;
            for (double xi = x; Ff(xi) > result; xi += 0.0001)
                result = Ff(xi);
            return result;
        }

        static double FUdobno(double x, double x0)
        {
            return Math.Sin(Math.Pow(x, 3)) / M(x0) + x;
        }

        static double F(double x)
        {
            return (double)(Math.Sin(Math.Pow(x, 3)) / 5);
        }

        static double Ff(double x)
        {
            return (double)(3 * Math.Pow(x, 2) * Math.Cos(Math.Pow(x, 3)) / 5);
        }

        static bool Positive(double x)
        {
            return Math.Sin(Math.Pow(x, 3)) >= 0;
        }

        static bool SolutionFound(double x)
        {
            return Positive(x) ^ Positive(x + dx);
        }

        static double Recursia(double x, int n)
        {
            if (n == 1)
                return FUdobno(x, x);
            else
                return Recursia(x, n - 1);
        }

        static void Color(double d)
        {
            if (d < 0)
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (d > 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(d);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {
            for (double i = x1; i < x2; i += dx)
            {
                Console.Write(DL.Left(i, 16) + "\t");
                Color(F(i));
                if (SolutionFound(i))
                {
                    Console.WriteLine("\t" + Recursia(i, 500) + "\t" + F(Recursia(i, 500)));
                }
            }
            Console.ReadKey();
        }
    }
}
