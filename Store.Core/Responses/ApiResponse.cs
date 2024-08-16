using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Responses
{
    public class ApiResponse(HttpStatusCode code, string? message = null)
    {
        public HttpStatusCode StatusCode { get; set; } = code;
        public string? Message { get; set; } = message;
    }
}
