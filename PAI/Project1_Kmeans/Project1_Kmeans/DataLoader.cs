using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Project1_Kmeans
{
    class DataLoader
    {
        public List<Wine> LoadWineData()
        {
            var wineDataSet = new List<Wine>();
            List<string> rows = LoadCsvFile("wineQuality-white.csv").Skip(1).ToList();
            foreach (string row in rows)
            {
                var dataRow = new Wine();
                List<string> attributes = row.Split(';').ToList();
                dataRow.FixedAcidity = Convert.ToDouble(attributes[0], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.VolatileAcidity = Convert.ToDouble(attributes[1], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.CitricAcid = Convert.ToDouble(attributes[2], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.ResidualSugar = Convert.ToDouble(attributes[3], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Chlorides = Convert.ToDouble(attributes[4], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.FreeSulfurDioxide = Convert.ToDouble(attributes[5], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.TotalSulfurDioxide = Convert.ToDouble(attributes[6], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Density = Convert.ToDouble(attributes[7], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Ph = Convert.ToDouble(attributes[8], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Sulphates = Convert.ToDouble(attributes[9], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Alcohol = Convert.ToDouble(attributes[10], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Quality = Convert.ToDouble(attributes[11], CultureInfo.InvariantCulture.NumberFormat);

                wineDataSet.Add(dataRow);

            }

            return wineDataSet;
        }

        public List<PokerHand> LoadPokerHandData()
        {
            var pokerHandDataSet = new List<PokerHand>();
            List<string> rows = LoadCsvFile("poker-hand-testing.csv").ToList();
            foreach (string row in rows)
            {
                var dataRow = new PokerHand();
                List<string> attributes = row.Split(',').ToList();
                dataRow.S1 = Convert.ToDouble(attributes[0], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.C1 = Convert.ToDouble(attributes[1], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.S2 = Convert.ToDouble(attributes[2], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.C2 = Convert.ToDouble(attributes[3], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.S3 = Convert.ToDouble(attributes[4], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.C3 = Convert.ToDouble(attributes[5], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.S4 = Convert.ToDouble(attributes[6], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.C4 = Convert.ToDouble(attributes[7], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.S5 = Convert.ToDouble(attributes[8], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.C5 = Convert.ToDouble(attributes[9], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Class = Convert.ToDouble(attributes[10], CultureInfo.InvariantCulture.NumberFormat);

                pokerHandDataSet.Add(dataRow);
            }

            return pokerHandDataSet;
        }

        private List<string> LoadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            var searchList = new List<string>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                searchList.Add(line);
            }

            return searchList;
        }
    }
}
