using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_2
{
    class Program
    {
        // 5 funkcí, hill climbing, blind search, 2 dimenze, 50x test a vybrat střední hodnotu
        static void Main(string[] args)
        {

            double[] startingPoint = { 2d, 2d };

            Hill_climbing hc = new Hill_climbing();
            hc.Start(startingPoint);
        }
    }
}
