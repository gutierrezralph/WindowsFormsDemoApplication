using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task.DTO.Request;
using Task.DTO.Response;
using Task.Windows.Forms.Helper;

namespace Task.Windows.Forms.Service
{
    public class EmployeeService
    {
        public static readonly UrlConfigSingleton Instance = new UrlConfigSingleton();
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<ResponseResult> GetAsync(string route)
        {
            var requestURL = string.Concat(Instance.Url, route);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = await reader.ReadToEndAsync();
                var serialized = JsonConvert.SerializeObject(result);
                var deserialized = JsonConvert.DeserializeObject<ResponseResult>(result);
                return deserialized;
            }
        }

        public async Task<EmployeeResponse> CreateServiceRequestAsync(string route, EmployeeRequest data, HttpRequestVerb method = HttpRequestVerb.POST)
        {
            var requestURL = string.Concat(Instance.Url, route);
            var serializedRequestData = JsonConvert.SerializeObject(data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(serializedRequestData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = method.ToString();

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var serialized = await reader.ReadToEndAsync();
                var deserialized = JsonConvert.DeserializeObject<EmployeeResponse>(serialized);
                return deserialized;
            }
        }

    }
}
