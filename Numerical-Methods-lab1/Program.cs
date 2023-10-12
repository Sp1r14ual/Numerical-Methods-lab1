using System;
using System.Diagnostics;
using System.Threading;


namespace Com_Methods
{
    class CONST
    {
        //точность решения
        public static double EPS = 1e-15;
    }

    class Program
    {
        static void Main()
        {
            try
            {
                //прямые методы: метод Гаусса, LU-разложение, QR-разложение

                int N = 200;
                var A = new Matrix(N, N);
                var X_true = new Vector(N);

                //заполнение СЛАУ
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i == j)
                            A.Elem[i][j] = 100;
                        else
                            A.Elem[i][j] = (0.1 + i + 0.5 * j);
                    }
                    X_true.Elem[i] = 1;
                }

                //правая часть
                var F = A * X_true;

                //решатель
                //var Solver = new Gauss_Method();
                //var Solver = new LU_Decomposition(A);
                var Solver = new QR_Decomposition(A, QR_Decomposition.QR_Algorithm.Givens);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                //var X = Solver.Start_Solver(A, F);
                var X = Solver.Start_Solver(F);

                stopwatch.Stop();

                Console.WriteLine("time = {0}", stopwatch.Elapsed);

                var result = X - X_true;
                var Delta = result.Norm() / X_true.Norm();

                Console.WriteLine("Delta = {0}", Delta);

                //for (int i = 0; i < X.N; i++) Console.WriteLine("X[{0}] = {1}", i + 1, X.Elem[i]);
            }
            catch (Exception E)
            {
                Console.WriteLine("\n*** Error! ***");
                Console.WriteLine("Method:  {0}", E.TargetSite);
                Console.WriteLine("Message: {0}\n", E.Message);
            }
        }
    }
}