using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv_1
{
    class ForecastRow
    {
        public Outlook Outlook { get; set; }
        public Temperature Temperature { get; set; }
        public Humidity Humidity { get; set; }
        public Windy Windy { get; set; }
        public Play Play { get; set; }

        public int YesCount { get; set; }
        public int NoCount { get; set; }
        public double Supp { get; set; }
        public double Conf { get; set; }

        public override string ToString()
        {
            string outlook = Outlook == Outlook.None ? "" : Outlook.ToString() + " + ";
            string temperature = Temperature == Temperature.None ? "" : Temperature.ToString() + " + ";
            string humidity = Humidity == Humidity.None ? "" : Humidity.ToString() + " + ";
            string windy = Windy == Windy.None ? "" : Windy.ToString();
            
            return outlook + temperature + humidity + windy;
        }
    }

    public enum Outlook
    {
        Rainy,
        Overcast,
        Sunny,
        None
    }
    public enum Temperature
    {
        Hot,
        Mild,
        Cool,
        None
    }
    public enum Humidity
    {
        High,
        Normal,
        None
    }
    public enum Windy
    {
        TRUE,
        FALSE,
        None
    }
    public enum Play
    {
        Yes,
        No
    }
}
