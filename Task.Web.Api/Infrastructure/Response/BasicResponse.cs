using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Web.Api.Infrastructure.Response
{
    public class BasicResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }
        public virtual object Response { get; set; }
    }
}