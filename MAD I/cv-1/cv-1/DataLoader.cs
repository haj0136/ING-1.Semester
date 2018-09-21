using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv_1
{
    class DataLoader
    {
        private readonly string data = @"Rainy,Hot,High,FALSE,No
Rainy, Hot, High, TRUE, No
Overcast,Hot,High,FALSE,Yes
Sunny, Mild, High, FALSE, Yes
Sunny,Cool,Normal,FALSE,Yes
Sunny, Cool, Normal, TRUE, No
Overcast,Cool,Normal,TRUE,Yes
Rainy, Mild, High, FALSE, No
Rainy,Cool,Normal,FALSE,Yes
Sunny, Mild, Normal, FALSE, Yes
Rainy,Mild,Normal,TRUE,Yes
Overcast, Mild, High, TRUE, Yes
Overcast,Hot,Normal,FALSE,Yes
Sunny, Mild, High, TRUE, No";

        public List<ForecastRow> LoadData()
        {
            List<ForecastRow> forecastData = new List<ForecastRow>();
            List<string> rows = data.Split('\n').ToList();
            foreach (string row in rows)
            {
                ForecastRow forecastRow = new ForecastRow();
                List<string> attributes = row.Split(',').ToList();
                forecastRow.Outlook = (Outlook)Enum.Parse(typeof(Outlook), attributes[0]);
                forecastRow.Temperature = (Temperature)Enum.Parse(typeof(Temperature), attributes[1]);
                forecastRow.Humidity = (Humidity)Enum.Parse(typeof(Humidity), attributes[2]);
                forecastRow.Windy = Boolean.Parse(attributes[3]);
                forecastRow.Play = (Play)Enum.Parse(typeof(Play), attributes[4]);
                forecastData.Add(forecastRow);
            }
            return forecastData;
        }
    }
}
