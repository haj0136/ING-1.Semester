using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    class Iris
    {
        // in cm
        public double Sepal_lenght { get; set; }
        public double Sepal_width { get; set; }
        public double Petal_lenght { get; set; }
        public double Petal_width { get; set; }
        public Iris_class Iris_type { get; set; }
    }
    public enum Iris_class
    {
        Iris_Setosa,
        Iris_Versicolour,
        Iris_Virginica
    }
}
