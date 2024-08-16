using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Responses
{
    public class ApiResponseWithData<T> : ApiResponse where T : class
    {
        public ApiResponseWithData(HttpStatusCode code, T? data, string? message = null) : base(code, message)
        {
            
        }

        public T? Data { get; set; }
    }
}
