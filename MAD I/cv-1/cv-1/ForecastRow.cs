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

        public override string ToString()
        {
            return Outlook + ", " + Temperature + ", " + Humidity + ", " + Windy + ", " + Play;
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
        True,
        False,
        None
    }
    public enum Play
    {
        Yes,
        No
    }
}
