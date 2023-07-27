using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Exceptions
{
    public class RestException:Exception
    {
        public RestException(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public RestException(HttpStatusCode code, string key, string errorMessage, string message = null)
        {
            Message = message;
            Code = code;
            Errors = new List<RestExceptionErrorItem> { new RestExceptionErrorItem(key, errorMessage) };
        }

        public RestException(HttpStatusCode code, string message, List<RestExceptionErrorItem> errors)
        {
            Code = code;
            Message = message;
            Errors = errors;
        }

        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public List<RestExceptionErrorItem> Errors { get; set; }
    }

    public class RestExceptionErrorItem
    {
        public RestExceptionErrorItem(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public string Key { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
