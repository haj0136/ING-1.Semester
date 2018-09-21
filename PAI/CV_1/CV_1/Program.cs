using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int NoUrls = 3308; // 3308 MAX

            Stopwatch sw = new Stopwatch();
            ServicePointManager.DefaultConnectionLimit = 400;
            sw.Start();
            ParalelDownloader downloader = new ParalelDownloader(NoUrls);
            List<PersonWrapper> people = downloader.StartDownloading();

            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds);

            foreach (var item in people)
            {
                Console.WriteLine(item.ToString());
            }


            /*
            Person person = new Person();
            person.GivenName = "Edward";
            person.Id = "Zwick";
            person.Roles = "D";
            person.FamilyName = "Zwick";

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            ser.WriteObject(stream, person);

            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());
            */

        }
    }
}
