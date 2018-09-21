using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            FindAllPossiblePermutations("ABCDEFGHIJKL", 'A');

            watch.Stop();

            System.Console.WriteLine( "Time elapsed: " + watch.ElapsedMilliseconds + "ms");
        }

        static void FindAllPossiblePermutations(string cities, char firstNode, int step = 0)
        {
            if(step == 0)
            {
                cities = cities.Replace(firstNode.ToString(), string.Empty);
            }
            //if (step == cities.Length)
            //{
            //    Console.WriteLine(firstNode + cities);
            //}

            foreach (int i in Enumerable.Range(step, cities.Length - step)){


                var cities_copy = SwapCharacters(cities, step, i);

                FindAllPossiblePermutations(cities_copy, firstNode, step + 1);
            }
        }

        static string SwapCharacters(string value, int position1, int position2)
        {
            // Swaps characters in a string.
            char[] array = value.ToCharArray(); 
            char temp = array[position1]; 
            array[position1] = array[position2]; 
            array[position2] = temp; 
            return new string(array); 
        }
    }
}
