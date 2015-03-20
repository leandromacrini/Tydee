using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tydee.Models
{
    public class ApiResponse
    {
        public bool Error;
        public string Message;
        public object Data;

        public ApiResponse(bool error = false, string message = "No errors", object data = null)
        {
            Error = error;
            Message = message;
            Data = data;
        }
    }
}