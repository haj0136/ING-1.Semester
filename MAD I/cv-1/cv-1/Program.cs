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
                //  Console.WriteLine(item.Outlook + ", " + item.Temperature + ", " + item.Humidity + ", " + item.Windy + ", " + item.Play);
               string rule = item.Outlook + " " + item.Temperature + " => " + item.Play;
            }
            


        }
    }
}
