using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Response
{
    public class Response
    {
        public ResponseCode Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
        public enum ResponseCode
        {
            Success = 200,
            NotFound = 404,
            BadRequest = 400,
            UnAuthorized = 401,
            Forbidden =403,
            InternalServerError = 500
        }
   
}
