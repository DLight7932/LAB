using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    class Matrix
    {
        int[,] A;
        int[] B;
        List<double[]> X;
        int N;
        int M;
        double D;

        public Matrix()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER:");
            Console.WriteLine("Enter dimensions");

            Program.Input(ref N, ref M);

            if (N > M || N < 2 || M < 2)
                throw new Exception("Input error!");

            ReadMatrix();
        }

        public void ReadMatrix()
        {
            Console.WriteLine();
            Console.WriteLine("Enter SLE");

            A = new int[N, M];
            B = new int[M];

            for (int i = 0; i < M; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length != N + 1)
                    throw new Exception("Input error!");

                for (int j = 0; j < N; j++)
                    A[j, i] = Convert.ToInt32(input[j]);

                B[i] = Convert.ToInt32(input[N]);
            }
        }

        public void WriteMatrix()
        {
            Console.WriteLine();
            Console.WriteLine("MATRIX:");

            for(int i = 0; i < M; i++)
            {
                Console.Write(A[0, i] + "X1");
                for (int j = 1; j < N; j++)
                {
                    Console.Write("\t");
                    if (A[j, i] >= 0)
                        Console.Write("+");
                    Console.Write(A[j, i] + "X" + (j + 1));
                }
                Console.WriteLine("\t" + "=" + "\t" + B[i]);
            }
        }

        public bool Convergence()
        {
            Console.WriteLine();
            Console.WriteLine("CONVERGENCE:");

            bool convergence = true;

            for (int i = 0; i < M; i++)
            {
                int iMax = 0;
                int sum = Math.Abs(A[0, i]);

                for (int j = 1; j < N; j++)
                {
                    sum += Math.Abs(A[j, i]);
                    if (Math.Abs(A[j, i]) > Math.Abs(A[iMax, i]))
                        iMax = j;
                }

                Console.Write("|" + A[iMax, i] + "|" + "\t" + ">" + "\t");

                for (int j = 0; j < N; j++)
                {
                    if (iMax != j)
                    {
                        Console.Write("|" + A[j, i] + "|");
                        if (j != N - 1 && !(iMax == N - 1 && j == N - 2))
                            Console.Write("\t" + "+" + "\t");
                    }
                }

                Console.Write("\t");

                Console.Write(Math.Abs(A[iMax, i]) > sum - Math.Abs(A[iMax, i]));

                convergence &= Math.Abs(A[iMax, i]) > sum - Math.Abs(A[iMax, i]);

                Console.WriteLine();
            }
            Console.WriteLine("Convergence: " + convergence);

            return convergence;
        }

        public void IterativeFormulas()
        {
            Console.WriteLine();
            Console.WriteLine("ITERATIVE FORMULAS:");

            for (int i = 0; i < N; i++)
            {
                string length = "";
                Console.Write("\t");
                length += Convert.ToString(B[i]);
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                    {
                        if (A[j, i] <= 0)
                            length += "+";
                        length += Convert.ToString(-A[j, i]);
                        length += Convert.ToString("X" + (j + 1));
                    }
                }
                Console.WriteLine(length);

                Console.WriteLine("X   = \t" + Program.Mult("─", length.Length));

                Console.WriteLine(" " + (i + 1) + "\t" + Program.Mult(" ", (length.Length - Convert.ToString(A[i, i]).Length) / 2) + A[i, i]);
            }
        }

        public void Iteration()
        {
            Console.WriteLine();
            Console.WriteLine("ITERATION:");

            X = new List<double[]>();
            X.Add(new double[N]);
            for (int i = 0; i < N; i++)
                X[0][i] = 0;

            Console.Write("X1=" + X[0][0]);
            for (int j = 1; j < N; j++)
                Console.Write(" X" + (j + 1) + "=" + X[0][j]);
            Console.WriteLine();

            Console.WriteLine("Enter count of iterations");
            int I = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
            for (int i = 0; i < I; i++)
            {
                Console.WriteLine("Iteration " + (i + 1) + ":");

                X.Add(new double[N]);
                for (int j = 0; j < N; j++)
                    X[i + 1][j] = X[i][j];

                for (int j = 0; j < N; j++)
                {
                    string length = "";
                    double Xi = B[j];
                    Console.Write("\t");
                    length += Convert.ToString(B[j]);
                    for (int k = 0; k < N; k++)
                    {
                        if (k != j)
                        {
                            if (A[k, j] <= 0)
                                length += "+";
                            length += Convert.ToString(-A[k, j]);
                            length += "*";
                            length += Convert.ToString(X[i + 1][k]);
                            Xi += -A[k, j] * X[i + 1][k];
                        }
                    }
                    Console.WriteLine(length);

                    Xi /= A[j, j];
                    Console.WriteLine("X   = \t" + Program.Mult("─", length.Length) + "\t=\t" + Xi);
                    X[i + 1][j] = Xi;

                    Console.WriteLine(" " + (j + 1) + "\t" + Program.Mult(" ", (length.Length - Convert.ToString(A[j, j]).Length) / 2) + A[j, j]);
                }
            }

            Console.Write("X1=" + X[X.Count - 1][0]);
            for (int j = 1; j < N; j++)
                Console.Write(" X" + (j + 1) + "=" + X[X.Count - 1][j]);
            Console.WriteLine();
        }

        public void AccuracyDetermination()
        {
            Console.WriteLine();
            Console.WriteLine("ACCURACY DETERMINATION:");

            D = 0;
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("D" + (i + 1) + "=" + "|" + X[X.Count - 1][i] + "-" + X[X.Count - 2][i] + "|" + "=" + Math.Abs(X[X.Count - 1][i] - X[X.Count - 2][i]));
                if (D < Math.Abs(X[X.Count - 1][i] - X[X.Count - 2][i]))
                    D = Math.Abs(X[X.Count - 1][i] - X[X.Count - 2][i]);
            }
            Console.WriteLine("D=" + D);
        }

        public void Verification()
        {
            Console.WriteLine();
            Console.WriteLine("VERIFICATION:");

            for (int i = 0; i < M; i++)
            {
                Console.Write(A[0, i] + "X1");
                double Xd = A[0, i] * X[X.Count - 1][0];
                for (int j = 1; j < N; j++)
                {
                    Console.Write("\t");
                    if (A[j, i] >= 0)
                        Console.Write("+");
                    Console.Write(A[j, i] + "X" + (j + 1));
                    Xd += A[j, i] * X[X.Count - 1][j];
                }
                Console.WriteLine("\t=\t" + Xd + "\t~\t" + B[i]);
            }
        }
    }

    class Program
    {
        public static string Mult(string s, int c)
        {
            string result = "";
            for (int i = 0; i < c; i++)
                result += s;
            return result;
        }

        public static void Input(ref int int1, ref int int2)
        {
            try
            {
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length != 2) 
                    throw new Exception();

                int1 = Convert.ToInt32(input[0]);
                int2 = Convert.ToInt32(input[1]);
            }
            catch
            {
                Console.WriteLine("Input error!");
                throw new Exception();
            }
        }

        static void Main(string[] args)
        {
            Matrix matrix;
            while (true)
            {
                try
                {
                    Console.WriteLine("-=Seidel's method=-");
                    matrix = new Matrix();
                    matrix.WriteMatrix();
                    if (!matrix.Convergence())
                        throw new Exception("SLE is not Convergentive!");
                    matrix.IterativeFormulas();
                    matrix.Iteration();
                    matrix.AccuracyDetermination();
                    matrix.Verification();
                    break;
                }
                catch(Exception exeption)
                {
                    Console.WriteLine();
                    ConsoleColor C = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exeption.Message);
                    Console.ForegroundColor = C;
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.ReadKey();
        }
    }
}
