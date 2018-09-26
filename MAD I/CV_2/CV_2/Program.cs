using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_2
{
    // prumerny vektor (mí)
    // rozptyl var
    // euklidovské vzdálenost
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<Iris> IrisList = dl.LoadData();

            foreach (var item in IrisList)
            {
                
            }
        }
    }
}
