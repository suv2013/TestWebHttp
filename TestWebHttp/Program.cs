using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestWebHttp
{
    class Program
    {
        static void Main(String[] args)
        {
            string s = Console.ReadLine();

            string fileName = System.Environment.GetEnvironmentVariable("OUTPUT_PATH");
            //TextWriter fileOut = new StreamWriter(@fileName, true);

             getMovieTitles(s);
            //for (int res_i = 0; res_i < res.Length; res_i++)
            //{
            //    fileOut.WriteLine(res[res_i]);
            //}

            //fileOut.Flush();
            //fileOut.Close();
        }

        private static void getMovieTitles(string s)
        {
            string Uri = "https://jsonmock.hackerrank.com/api/movies/search/?Title=";
            Uri = Uri + s;
            string all = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Uri);
            httpWebRequest.Method = "GET";

            httpWebRequest.Timeout = 5000;

            var respose = (HttpWebResponse)httpWebRequest.GetResponse();

            var responseSteam = respose.GetResponseStream();

            if (responseSteam != null)
            {
                var reader = new StreamReader(responseSteam);
                all = reader.ReadToEnd();
            }
            if (responseSteam != null)
            {
                responseSteam.Close();
            }

            movieData alldata = Newtonsoft.Json.JsonConvert.DeserializeObject<movieData>(all);
            movieDetails[] moviedatas = alldata.data;
            string[] movieNames = new string[alldata.total];
            int i = 0;
            foreach(movieDetails movdata in moviedatas)
            {
                movieNames[i] = movdata.Title;
                i++;
            }

            Uri = Uri + "&page=2";
            
            httpWebRequest = (HttpWebRequest)WebRequest.Create(Uri);
            httpWebRequest.Method = "GET";

            httpWebRequest.Timeout = 5000;

            respose = (HttpWebResponse)httpWebRequest.GetResponse();

            responseSteam = respose.GetResponseStream();

            if (responseSteam != null)
            {
                var reader = new StreamReader(responseSteam);
                all = reader.ReadToEnd();
            }
            if (responseSteam != null)
            {
                responseSteam.Close();
            }

            alldata = Newtonsoft.Json.JsonConvert.DeserializeObject<movieData>(all);
            moviedatas = alldata.data;
            
            foreach (movieDetails movdata in moviedatas)
            {
                movieNames[i] = movdata.Title;
                i++;
            }

            Array.Sort(movieNames);
        }

        public class movieData
        {
             public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public int total_pages { get; set; }
            public movieDetails[] data { get; set; }
        }

        public class movieDetails
        {

            public string Poster { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public int Year { get; set; }
            public string imdbID { get; set; }

        }
        
    }
}
