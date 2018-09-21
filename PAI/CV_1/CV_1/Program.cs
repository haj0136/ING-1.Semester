using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int NoUrls = 3308;
            const string URL = "http://name-service.appspot.com/api/v1/names/";

            Task[] tasks = new Task[NoUrls];

            for (int i = 0; i <= NoUrls; i++)
            {
                tasks[i] = Task.Factory.StartNew( state =>
                {
                    using (var client = new WebClient())
                    {

                    }
                }, )
            }

            Task.WaitAll(tasks);

        }
    }
}
