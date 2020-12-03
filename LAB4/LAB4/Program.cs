using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLibrary;

namespace LAB4
{
    class Program
    {
        static int[] X;
        static int[] Y;
        static int n;

        static void LagrangePolynomial()
        {
            Console.WriteLine("-=Interpolation Lagrange Polynomial=-");

            Console.WriteLine();
            Console.WriteLine("LAGRANGE MULTIPLIER:");
            for (int i = 0; i < n; i++)
            {
                string numerator1, numerator2;
                if (i == 1)
                {
                    numerator1 = "(x-x0)";
                    numerator2 = "(x-" + X[0] + ")";
                }
                else if (i == 0)
                {
                    numerator1 = "(x-x1)";
                    numerator2 = "(x-" + X[1] + ")";
                }
                else
                {
                    numerator1 = "(x-x0)*(x-x1)";
                    numerator2 = "(x-" + X[0] + ")*(x-" + X[1] + ")";
                }
                for (int j = 2; j < n; j++)
                    if (j != i)
                    {
                        numerator1 += "*(x-x" + j + ")";
                        numerator2 += "*(x-" + X[j] + ")";
                    }

                string denominator1, denominator2;
                if (i == 1)
                {
                    denominator1 = "(xi-x0)";
                    denominator2 = "(xi-" + X[0] + ")";
                }
                else if (i == 0)
                {
                    denominator1 = "(xi-x1)";
                    denominator2 = "(xi-" + X[1] + ")";
                }
                else
                {
                    denominator1 = "(xi-x0)*(xi-x1)";
                    denominator2 = "(xi-" + X[0] + ")*(xi-" + X[1] + ")";
                }
                for (int j = 2; j < n; j++)
                    if (j != i)
                    {
                        denominator1 += "*(xi-x" + j + ")";
                        denominator2 += "*(xi-" + X[j] + ")";
                    }

                Console.WriteLine(DL.Mult(" ", Convert.ToString("l" + i + "(x)" + " = ").Length) + DL.Mult(" ", (denominator1.Length - numerator1.Length + 1) / 2) + numerator1 + DL.Mult(" ", (denominator1.Length - numerator1.Length) / 2 + 3 + (denominator2.Length - numerator2.Length) / 2) + numerator2);
                Console.WriteLine("l" + i + "(x)" + " = " + DL.Mult("─", denominator1.Length) + " = " + DL.Mult("─", denominator2.Length));
                Console.WriteLine(DL.Mult(" ", Convert.ToString("l" + i + "(x)" + " = ").Length) + denominator1 + "   " + denominator2);

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("POLYNOMIAL:");
            Console.Write("P(x) = ");
            Console.Write(Y[0] + "*l0");
            for (int i = 1; i < n; i++)
                Console.Write(" + " + Y[i] + "*" + "l(x)" + i);

            Console.WriteLine();
        }

        static void SplineInterpolation()
        {
            double[] A = new double[n];
            double[] B = new double[n];
            double[] C = new double[n];
            double[] D = new double[n];

            double S(int i, double x)
            {
                return A[i] + B[i] * (x - X[0]) + C[i] * (x - X[0]) + D[i] * (x - X[0]);
            }

            string P(string c, double x)
            {
                if (x != 1)
                    if (x > 0)
                        return c + Convert.ToString(x);
                    else if (c == "-")
                        return c + Convert.ToString(-x);
                    else
                        return c + Convert.ToString(-x);
                return c;
            }

            void Draw0()
            {
                Console.WriteLine("Spline equation:");
                Console.WriteLine("Si = ai+bi(x-xi)^2+ci(x-xi)^2+di(x-xi)^2");
                Console.WriteLine();
            }

            void Draw1()
            {
                Console.WriteLine("Spline equations:");
                for (int i = 0; i < n - 1; i++)
                {
                    Console.WriteLine($"S{i} = a{i}+b(x" + P("-", X[i]) + ")^2+c(x" + P("-", X[i]) + ")^2+d(x" + P("-", X[i]) + ")^2");
                }
                Console.WriteLine();
            }

            void Draw2()
            {
                Console.WriteLine("Spline equations for key points:");
                for (int i = 0; i < n - 1; i++)
                {
                    Console.WriteLine("S" + i + ":");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{Y[i]} = ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"a{i}+b({X[i]}" + P("-", X[i]) + $")^2+c({X[i]}" + P("-", X[i]) + $")^2+d({X[i]}" + P("-", X[i]) + ")^2 = ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"a" + i);
                    Console.Write($"{Y[i + 1]} = ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"a{i}+b({X[i + 1]}" + P("-", X[i]) + $")^2+c({X[i + 1]}" + P("-", X[i]) + $")^2+d({X[i + 1]}" + P("-", X[i]) + ")^2 = ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("a" + i + P("+", X[i + 1] - X[i]) + "b" + i + P("+", Math.Pow(X[i + 1] - X[i], 2)) + "c" + i + P("+", Math.Pow(X[i + 1] - X[i], 3)) + "d" + i);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }

            void Draw1d()
            {
                Console.WriteLine("Spline first differential equation:");
                Console.WriteLine($"S'i = bi+2ci(x-xi)+3di(x-xi)^2");
                Console.WriteLine();
            }

            void Draw2d()
            {
                Console.WriteLine("First derivative spline equations:");
                for (int i = 0; i < n - 1; i++)
                    Console.WriteLine($"S'{i} = b{i}+2c{i}(x" + P("-", X[i]) + $")+3d{i}(x" + P("-", X[i]) + $")^2");
                Console.WriteLine();
            }

            void Drawdd()
            {
                Console.WriteLine("Equalizing derived splines:");
                for (int i = 0; i < n - 2; i++)
                {
                    Console.Write($"S{i}' = S{i + 1}'\t ");
                    Console.Write($"b{i}+2c{i}({X[i + 1]}" + P("-", X[i]) + $")+3d{i}({X[i + 1]}" + P("-", X[i]) + $")^2 = b{i + 1}+2c{i + 1}({X[i + 1]}" + P("-", X[i + 1]) + $")+3d{i + 1}({X[i + 1]}" + P("-", X[i + 1]) + ")^2\t ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"b{i}" + P("+", (X[i + 1] - X[i]) * 2) + $"c{i}" + P("+", Math.Pow(X[i + 1] - X[i], 2) * 3) + $"d{i} = b{i + 1}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void Draw1dd()
            {
                Console.WriteLine("Spline second differential equation:");
                Console.WriteLine($"S''i = 2ci+6di(x-xi)");
                Console.WriteLine();
            }

            void Draw2dd()
            {
                Console.WriteLine("First second derivative spline equations:");
                for (int i = 0; i < n - 1; i++)
                    Console.WriteLine($"S''{i} = 2c{i}+6d{i}(x" + P("-", X[i]) + ")");
                Console.WriteLine();
            }

            void Drawdddd()
            {
                Console.WriteLine("Equalizing derived splines:");
                for (int i = 0; i < n - 2; i++)
                {
                    Console.Write($"S{i}'' = S{i + 1}''\t ");
                    Console.Write($"2c{i}+6d{i}({X[i + 1]}" + P("-", X[i]) + $") = 2c{i + 1}+6d{i + 1}({X[i + 1]}" + P("-", X[i + 1]) + ")\t ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"2c{i}" + P("+", 6 * (X[i + 1] - X[i])) + $"d{i} = 2c{i + 1}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void Drawd_d()
            {
                Console.WriteLine("Spline boundaries behavior:");

                Console.Write("S''0(x0) = 0\t ");
                Console.Write($"2c0+6d0({X[0]}" + P("-", X[0]) + ") = 0\t ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("2c0 = 0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                Console.Write($"S''{n - 2}(x{n - 1}) = 0\t ");
                Console.Write($"2c{n - 2}+6d{n - 2}({X[n - 1]}" + P("-", X[n - 2]) + ") = 0\t ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"2c{n - 2}" + P("+", (X[n - 1] - X[n - 2]) * 6) + $"d{n - 2} = 0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                Console.WriteLine();
            }

            void DrawSystemOfEquations()
            {
                Console.WriteLine("System of equations:");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < n - 1; i++)
                {
                    Console.Write($"{Y[i]} = ");
                    Console.WriteLine($"a" + i);

                    Console.Write($"{Y[i + 1]} = ");
                    Console.WriteLine($"a" + i + P("+", X[i + 1] - X[i]) + "b" + i + P("+", Math.Pow(X[i + 1] - X[i], 2)) + "c" + i + P("+", Math.Pow(X[i + 1] - X[i], 3)) + "d" + i);
                }
                for (int i = 0; i < n - 2; i++)
                    Console.WriteLine($"b{i}" + P("+", (X[i + 1] - X[i]) * 2) + $"c{i}" + P("+", Math.Pow(X[i + 1] - X[i], 2) * 3) + $"d{i} = b{i + 1}");
                for (int i = 0; i < n - 2; i++)
                    Console.WriteLine($"2c{i}" + P("+", 6 * (X[i + 1] - X[i])) + $"d{i} = 2c{i + 1}");
                Console.WriteLine("2c0 = 0");
                Console.WriteLine($"2c{n - 2}" + P("+", (X[n - 1] - X[n - 2]) * 6) + $"d{n - 2} = 0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            void DrawSystemOfEquationsNormalized()
            {
                Console.WriteLine("System of equations normalized:");

                for (int i = 0; i < n - 1; i++)
                {
                    Console.Write($"{Y[i + 1] - Y[i]} = ");
                    Console.Write(X[i + 1] - X[i] + "b" + i);
                    if (i != 0)
                        Console.Write(P("+", Math.Pow(X[i + 1] - X[i], 2)) + "c" + i);
                    Console.WriteLine(P("+", Math.Pow(X[i + 1] - X[i], 3)) + "d" + i);
                }
                for (int i = 0; i < n - 2; i++)
                {
                    Console.Write($"0 = b{i}");
                    if (i != 0)
                        Console.Write(P("+", (X[i + 1] - X[i]) * 2) + $"c{i}");
                    Console.WriteLine(P("+", Math.Pow(X[i + 1] - X[i], 2) * 3) + $"d{i}-b{i + 1}");
                }
                for (int i = 0; i < n - 2; i++)
                {
                    Console.Write($"0 = ");
                    if (i != 0)
                        Console.Write($"2c{i}");
                    Console.WriteLine(P("+", 6 * (X[i + 1] - X[i])) + $"d{i}-2c{i + 1}");
                }
                Console.WriteLine($"0 = 2c{n - 2}" + P("+", (X[n - 1] - X[n - 2]) * 6) + $"d{n - 2}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("-=Spline Interpolation=-");

            Console.WriteLine();
            Console.WriteLine("SPLINE EQUATION:");
            Draw0();
            Draw1();
            Draw2();
            Draw1d();
            Draw2d();
            Drawdd();
            Draw1dd();
            Draw2dd();
            Drawdddd();
            Drawd_d();
            DrawSystemOfEquations();
            DrawSystemOfEquationsNormalized();
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("ENTER:");
            Console.WriteLine("Enter X");
            X = DL.Input();
            //X = new int[10] { -1, -2, -4, -7, 2, 3, 5, 6, 7, 9 };
            Console.WriteLine("Enter Y");
            Y = DL.Input();
            //Y = new int[10] { -2, -3, -1, -4, 4, 5, 2, 1, 3, 4 };
            n = 4;
            n = X.Length;
            if (Y.Length != n || n < 2)
                throw new Exception();
            Console.WriteLine();

            LagrangePolynomial();

            SplineInterpolation();

            Console.ReadKey();
        }
    }
}
