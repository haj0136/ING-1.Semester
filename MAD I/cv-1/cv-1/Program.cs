using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader dl = new DataLoader();
            List<ForecastRow> forecastData = dl.LoadData();
            
            foreach (ForecastRow item in forecastData)
            {
                //Console.WriteLine(item.ToString());
            }

            foreach (var outlook in Enum.GetValues(typeof(Outlook)))
            {
                foreach (var temperature in Enum.GetValues(typeof(Temperature)))
                {
                    foreach (var humidity in Enum.GetValues(typeof(Humidity)))
                    {
                        foreach (var windy in Enum.GetValues(typeof(Windy)))
                        {

                        }
                    }
                }

            }

        }
    }
}
