using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    class ParalelDownloader
    {
        private readonly int noUrls;
        private const string baseURL = "http://name-service.appspot.com/api/v1/names/";

        public ParalelDownloader(int _noUrls)
        {
            noUrls = _noUrls;
        }

        public List<PersonWrapper> StartDownloading()
        {
            Task<string>[] tasks = new Task<string>[noUrls + 1];

            for (int i = 0; i <= noUrls; i++)
            {
                string URL = baseURL + i + ".json";

                tasks[i] = Task.Factory.StartNew(state =>
                {
                    using (var client = new WebClient())
                    {
                        var u = (string)state;
                        //Console.WriteLine("starting to download {0}", u);
                        string result = client.DownloadString(u);
                        //Console.WriteLine("finished downloading {0}", u);
                        return result;
                    }
                }, URL);
            }

            Task.WaitAll(tasks);
            List<PersonWrapper> people = new List<PersonWrapper>();
            foreach (var t in tasks)
            {
                people.Add(ReadToObject(t.Result));
            }

            return people;
        }
        public PersonWrapper ReadToObject(string json)
        {
            PersonWrapper deserializedUser;
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PersonWrapper));
            deserializedUser = ser.ReadObject(ms) as PersonWrapper;
            ms.Close();
            return deserializedUser;
        }
    }
}
