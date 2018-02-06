using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.DTO.Response
{
    public class ResponseResult
    {
        public List<object> Errors { get; set; }
        public Result Result { get; set; }
    }

    public class ResponseData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class Result
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }

        [JsonProperty("Response")]
        public List<ResponseData> ResponseData { get; set; }
    }
}
