using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cv_6
{
    class DataLoader
    {
        public List<Iris> LoadData()
        {
            List<Iris> IrisDataSet = new List<Iris>();
            List<string> rows = LoadCsvFile("iris.data.txt");
            foreach (string row in rows)
            {
                Iris dataRow = new Iris();
                List<string> attributes = row.Split(',').ToList();
                dataRow.Sepal_lenght = Convert.ToDouble(attributes[0], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Sepal_width = Convert.ToDouble(attributes[1], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Petal_lenght = Convert.ToDouble(attributes[2], CultureInfo.InvariantCulture.NumberFormat);
                dataRow.Petal_width = Convert.ToDouble(attributes[3], CultureInfo.InvariantCulture.NumberFormat);
                switch (attributes[4])
                {
                    case "Iris-setosa": dataRow.Iris_type = Iris_class.Iris_Setosa; break;
                    case "Iris-versicolor": dataRow.Iris_type = Iris_class.Iris_Versicolour; break;
                    case "Iris-virginica": dataRow.Iris_type = Iris_class.Iris_Virginica; break;
                }
                IrisDataSet.Add(dataRow);
            }
            return IrisDataSet;
        }

        private List<string> LoadCsvFile(string filePath)
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
    }
}
