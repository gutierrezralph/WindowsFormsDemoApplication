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
        public static readonly UrlConfigSingleton _instance = new UrlConfigSingleton();
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Get all available data
        /// </summary>
        /// <returns>Employee records</returns>
        public async Task<ResponseResult> GetAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_instance.Url);
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

        /// <summary>
        /// Sending data to API
        /// </summary>
        /// <param name="uriRoute">Employee API route</param>
        /// <param name="requestData">Data that will be send thru API</param>
        /// <param name="method">http method ex.POST,GET,DELETE,PUT</param>
        /// <returns></returns>
        public async Task<EmployeeResponse> SendingRequestAsync(string uriRoute, EmployeeRequest requestData, HttpRequestMethod method)
        {
            var requestURL = string.Concat(_instance.Url, uriRoute);
            var serializedRequestData = JsonConvert.SerializeObject(requestData);
            byte[] dataBytes = Encoding.UTF8.GetBytes(serializedRequestData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = method.ToString();

            using (Stream requestBody = request.GetRequestStream()) await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
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
