using System.Globalization;
using System.Net;

namespace RedacteurPortaal.DomainModels;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

    public AppException() : base()
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
