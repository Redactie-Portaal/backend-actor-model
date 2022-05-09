using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, HttpStatusCode statuscode) : base(message)
        {
            this.StatusCode = statuscode;
        }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public AppException(string message, HttpStatusCode statusCode, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            this.StatusCode = statusCode;
        }
    }
}