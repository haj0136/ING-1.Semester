using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Kmeans
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();

            var dl = new DataLoader();
            List<PokerHand> pokerHandList = dl.LoadPokerHandData();

            sw.Start();
            var kMeans = new KMeans(pokerHandList);
            List<PokerHand> clusteredIrisList = kMeans.GetClusters();
            sw.Stop();

            Console.WriteLine("Single thread time = " + sw.Elapsed.TotalSeconds + " sec");

            sw.Reset();
            sw.Start();
            var kMeansP = new KMeansParallel(pokerHandList);
            clusteredIrisList = kMeansP.GetClusters();
            sw.Stop();

            Console.WriteLine("Multi thread time = " + sw.Elapsed.TotalSeconds + " sec");

        }
    }
}
