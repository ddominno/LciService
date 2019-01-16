using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DriscTAF.Utilities
{
    using System.IO;

    public class JsonManager
    {
        public static string JsonString(string path)
        {
            string json = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    json = json + sr.ReadLine();
                }
            }
            return json;
        }

        public static string ModelToPayload(object model)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }
    }
}

