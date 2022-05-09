using RedacteurPortaal.DomainModels;
using System.Net;
using System.Text.Json;

namespace RedacteurPortaal.Api.Middleware
{
    public class ExceptionHandelingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandelingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)e.StatusCode;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var message = error.Message;

                if (error is not AppException)
                {
                    message = response.StatusCode switch {
                        (int)HttpStatusCode.NotFound => "Destination or resource not found.",
                        _ => "An internal server error occured",
                    };
                }

                var result = JsonSerializer.Serialize(new { message });
                await response.WriteAsync(result);
            }
        }
    }
}