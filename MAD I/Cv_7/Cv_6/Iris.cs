using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    public class Iris
    {
        public int Id { get; set; }
        // in cm
        public double Sepal_lenght { get; set; }
        public double Sepal_width { get; set; }
        public double Petal_lenght { get; set; }
        public double Petal_width { get; set; }
        public Iris_class Iris_type { get; set; }
        public int ClusterIndex { get; set; }

        public Iris()
        {
            ClusterIndex = 0;
        }

        public Iris(Iris iris)
        {
            Sepal_lenght = iris.Sepal_lenght;
            Sepal_width = iris.Sepal_width;
            Petal_lenght = iris.Petal_lenght;
            Petal_width = iris.Petal_width;
            Iris_type = iris.Iris_type;
        }

        /// <summary>
        /// values in array: Sepal lenght; Sepal width; Petal lenght; Petal width;
        /// </summary>
        public double[] ToArray()
        {
            return new double[] { Sepal_lenght, Sepal_width , Petal_lenght, Petal_width };
        }
    }

    public enum Iris_class
    {
        Iris_Setosa,
        Iris_Versicolour,
        Iris_Virginica
    }

}
