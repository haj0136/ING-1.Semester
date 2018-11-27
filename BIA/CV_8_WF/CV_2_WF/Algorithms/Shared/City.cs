using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_7_WF.Algorithms.Shared
{
    public class City
    {
        public List<int> X { get; set; }
        public int Index { get; set; }
        public char Name { get; set; }

        public City()
        {
            X = new List<int>();
        }

        public int[] ToIntArray()
        {
            return X.ToArray();
        }


    }
}
