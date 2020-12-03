using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLibrary;

namespace LAB3
{
    class Function
    {
        virtual public double this[double[] X]
        {
            get
            {
                return 0;
            }
        }
    }

    class F0 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return 2 * X[0] - 4 * Math.Pow(X[1], 3) - 1.5;
            }
        }
    }

    class F1 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return Math.Pow(X[0], 3) - 5 * Math.Pow(X[1], 2) + 0.25;
            }
        }
    }

    class W00 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return 2.0;
            }
        }
    }

    class W01 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return -12 * Math.Pow(X[1], 2);
            }
        }
    }
    class W10 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return 3 * Math.Pow(X[0], 2);
            }
        }
    }
    class W11 : Function
    {
        public override double this[double[] X]
        {
            get
            {
                return -10 * X[1];
            }
        }
    }

    class Program
    {
        static List<double[]> X = new List<double[]>();
        static Function[] F = { new F0(), new F1() };
        static Function[,] W = { { new W00(), new W01() }, { new W10(), new W11() } };

        static void Print(double[] d)
        {
            for (int i = 0; i < d.Length; i++)
                Console.Write("X" + (i + 1) + "=" + d[i] + " ");
            Console.WriteLine();
        }

        static void Iteration()
        {
            X.Add(new double[2]);
            X[X.Count - 1][0] -= W[0, 0][X[X.Count - 2]] * F[0][X[X.Count - 2]] + W[0, 1][X[X.Count - 2]] * F[1][X[X.Count - 2]];
            X[X.Count - 1][1] -= W[1, 0][X[X.Count - 2]] * F[0][X[X.Count - 2]] + W[1, 1][X[X.Count - 2]] * F[1][X[X.Count - 2]];
        }

        static void Main(string[] args)
        {
            Console.WriteLine("-=Newton's method=-");

            X.Add(new double[2]);
            for (int i = 0; i < 2; i++)
                X[0][i] = 0;
            DL.Input(ref X[0][0], ref X[0][1]);

            Console.WriteLine("Enter count of iterations");
            int I = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("ITERATION");
            Print(X[0]);
            for (int i = 0; i < I; i++)
            {
                Console.WriteLine("Iteration " + (i + 1) + ":");
                Iteration();
                Print(X[X.Count - 1]);
            }

            Console.ReadKey();
        }
    }
}
