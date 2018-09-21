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
        public bool Windy { get; set; }
        public Play Play { get; set; }

    }

    public enum Outlook
    {
        Rainy,
        Overcast,
        Sunny
    }
    public enum Temperature
    {
        Hot,
        Mild,
        Cool
    }
    public enum Humidity
    {
        High,
        Normal
    }
    public enum Play
    {
        Yes,
        No
    }
}
