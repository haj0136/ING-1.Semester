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
            

            List<ForecastRow> rules = new List<ForecastRow>();
 
            foreach (var outlook in Enum.GetValues(typeof(Outlook)))
            {
                foreach (var temperature in Enum.GetValues(typeof(Temperature)))
                {
                    foreach (var humidity in Enum.GetValues(typeof(Humidity)))
                    {
                        foreach (var windy in Enum.GetValues(typeof(Windy)))
                        {
                            ForecastRow rule = new ForecastRow
                            {
                                Outlook = (Outlook)outlook,
                                Temperature = (Temperature)temperature,
                                Humidity = (Humidity)humidity,
                                Windy = (Windy)windy
                            };
                            rules.Add(rule);
                        }
                    }
                }

            }

            foreach (ForecastRow item in forecastData)
            {
                //Console.WriteLine(item.ToString());
                foreach (var rule in rules)
                {
                    if ((item.Outlook == rule.Outlook && item.Temperature == rule.Temperature && item.Humidity == rule.Humidity && item.Windy == rule.Windy) || 
                        (item.Outlook == rule.Outlook && item.Temperature == rule.Temperature && item.Humidity == rule.Humidity && rule.Windy == Windy.None) ||
                        (item.Outlook == rule.Outlook && item.Temperature == rule.Temperature && rule.Humidity == Humidity.None && rule.Windy == Windy.None) ||
                        (item.Outlook == rule.Outlook && rule.Temperature == Temperature.None && rule.Humidity == Humidity.None && rule.Windy == Windy.None) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == item.Temperature && rule.Humidity == Humidity.None && rule.Windy == Windy.None) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == item.Temperature && rule.Humidity == item.Humidity && rule.Windy == Windy.None) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == item.Temperature && rule.Humidity == item.Humidity && rule.Windy == item.Windy) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == Temperature.None && rule.Humidity == item.Humidity && rule.Windy == Windy.None) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == Temperature.None && rule.Humidity == item.Humidity && rule.Windy == item.Windy) ||
                        (rule.Outlook == Outlook.None && rule.Temperature == Temperature.None && rule.Humidity == Humidity.None && rule.Windy == item.Windy))                     {
                        if (item.Play == Play.No)
                            rule.NoCount++;
                        else
                            rule.YesCount++;

                    }
                }
            }

            foreach (var rule in rules){
                double r = rule.NoCount + rule.YesCount;
                double N = forecastData.Count;
                double Cmax = rule.NoCount < rule.YesCount ? rule.YesCount : rule.NoCount;

                if (N != 0)
                    rule.Supp = r / N;

                if (r != 0)
                    rule.Conf = Cmax / r;

                if (rule.YesCount > 0 || rule.NoCount > 0)
                {
                    Console.WriteLine(rule.ToString() + " => yes: " + rule.YesCount + "  no: " + rule.NoCount + " supp = " + rule.Supp.ToString("#0.000") + " conf = " + rule.Conf.ToString("#0.000"));
                }
            }
	

	
            

        }
    }
}
