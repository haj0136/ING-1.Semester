using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<Iris> IrisList = dl.LoadData();

            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double x4 = 0;

            foreach (var item in IrisList)
            {
                x1 += item.Petal_lenght;
                x2 += item.Petal_width;
                x3 += item.Sepal_lenght;
                x4 += item.Sepal_width;
            }

            double[] mean = { x1 / IrisList.Count(), x2 / IrisList.Count(), x3 / IrisList.Count(), x4 / IrisList.Count() };

            double variance = 0;

            foreach (var item in IrisList)
            {
                double[] temp = { item.Petal_lenght, item.Petal_width, item.Sepal_lenght, item.Sepal_width };
                variance += Math.Pow(EuclideanDistance(temp, mean), 2);
            }

            variance = variance / IrisList.Count;

            Console.WriteLine(variance);
        }

        static double EuclideanDistance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(result);
        }
    }
    
}
