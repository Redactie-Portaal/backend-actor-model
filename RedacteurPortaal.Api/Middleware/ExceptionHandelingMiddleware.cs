﻿using RedacteurPortaal.DomainModels;
using System.Net;
using System.Text.Json;

namespace RedacteurPortaal.Api.Middleware
{
    public class ExceptionHandelingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionHandelingMiddleware(RequestDelegate next, ILogger<ExceptionHandelingMiddleware> logger)
        {
            this.logger = logger;
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
                        this.logger.LogError($"An exception occured: {e.Message} at {e.StackTrace}");
                        response.StatusCode = (int)e.StatusCode;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        this.logger.LogError($"A not found exception occured: {e.Message} at {e.StackTrace}");
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        this.logger.LogError($"An internal server error occurred: {error.Message} at {error.StackTrace}");
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