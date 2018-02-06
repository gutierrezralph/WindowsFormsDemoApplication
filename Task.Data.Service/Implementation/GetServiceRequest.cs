using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task.Data.Service.Helper;

namespace Task.Data.Service.Implementation
{
    public class GetServiceRequest
    {
        public static readonly UrlConfigSingleton _instance = new UrlConfigSingleton();

        public async Task<IEnumerable<T>> GetAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_instance.Url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var serialized = await reader.ReadToEndAsync();
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<T>>(serialized);
                return deserialized;
            }
        }
    }
}
