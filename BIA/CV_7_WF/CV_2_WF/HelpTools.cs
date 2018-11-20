using CV_7_WF.Algorithms.GA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CV_4_WF
{
    public static class HelpTools
    {
        public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static List<City> LoadCSVData(string filepath)
        {
            var cities = new List<City>();
            List<string> rows = LoadCsvFile(filepath);
            foreach (string row in rows)
            {
                var dataRow = new City();
                var attributes = row.Split(',').ToList();
                dataRow.X.Add(Convert.ToInt32(attributes[0]));
                dataRow.X.Add(Convert.ToInt32(attributes[1]));
                dataRow.Name = Convert.ToChar(attributes[2].Replace("'", ""));
                dataRow.Index = Convert.ToInt32(attributes[3]);

                cities.Add(dataRow);
            }
            return cities;
        }

        private static List<string> LoadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> searchList = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }

            return searchList;
        }

        public static double EuclideanDistance(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += Math.Pow(a[i] - b[i], 2);
            }

            return Math.Sqrt(result);
        }
        public static double EuclideanDistance(int[] a, int[] b)
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
