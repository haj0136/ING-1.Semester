using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_LU_Decomposition
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new Matrix(500);
            matrix.MakeRandomMatrix();
            //matrix.PrintMatrix();

            var watch = new Stopwatch();
            watch.Start();
            var LU = matrix.LuDecomposition();
            watch.Stop();
            Console.WriteLine($"Single thread time: {watch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine();
            //LU.U.PrintMatrix();
            //LU.L.PrintMatrix();

            watch.Reset();
            watch.Start();
            var LU2 = matrix.ParallelLuDecomposition();
            watch.Stop();
            Console.WriteLine($"Multi thread time: {watch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine();
            //LU2.U.PrintMatrix();
            //LU2.L.PrintMatrix();

            Console.ReadKey();
        }
    }
}
