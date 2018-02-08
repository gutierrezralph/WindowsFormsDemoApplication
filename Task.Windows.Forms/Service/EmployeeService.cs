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

        /// <summary>
        /// Creating request to API
        /// </summary>
        /// <param name="uriRoute">
        /// URI route for employee
        /// Ex: api/employee/get
        /// </param>
        /// <returns> All records from employee table </returns>
        public async Task<ResponseResult> GetRequestAsync(string uriRoute)
        {
            var requestURL = string.Concat(_instance.Url, uriRoute);
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

        /// <summary>
        /// Creating request from API
        /// </summary>
        /// <param name="uriRoute">
        /// URI route for employee
        /// Ex: api/employee/Add
        /// Ex: api/employee/Edit{id}
        /// Ex: api/employee/Remove/{id}
        /// </param>
        /// <param name="employeeRequest"> Contains data informations that will be sent thru Api </param>
        /// <param name="httpRequestMethod">Http request method</param>
        /// <returns></returns>
        public async Task<EmployeeResponse> CreateRequestAsync(string uriRoute, EmployeeRequest employeeRequest, HttpRequestVerbEnum httpRequestMethod = HttpRequestVerbEnum.Post)
        {
            var requestURL = string.Concat(_instance.Url, uriRoute);
            var serializedRequestData = JsonConvert.SerializeObject(employeeRequest);
            byte[] dataBytes = Encoding.UTF8.GetBytes(serializedRequestData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = httpRequestMethod.ToString();

            using (Stream requestBody = request.GetRequestStream())
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);

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
