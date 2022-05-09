using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RedacteurPortaal.Api.Middleware;
using RedacteurPortaal.DomainModels;
using Xunit;
using Xunit.Sdk;

namespace RedacteurPortaal.Tests.Api.Middleware
{
    [Collection("Col")]
    public class ExceptionHandelingMiddlewareTests
    {
        [Fact]
        public async Task MapAppExceptionToGivenException()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder => {
                    webBuilder
                        .UseTestServer()
                        .Configure(app => {
                            
                            app.UseMiddleware<ExceptionHandelingMiddleware>();
                            app.Run( (app) => {
                                throw new AppException("foo", HttpStatusCode.Accepted);
                            });
                        });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync($"api/Address");

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task MapKeyNotFoundTo404()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder => {
                    webBuilder
                        .UseTestServer()
                        .Configure(app => {

                            app.UseMiddleware<ExceptionHandelingMiddleware>();
                            app.Run((app) => {
                                throw new KeyNotFoundException();
                            });
                        });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync($"api/Address");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task MapUnknownTo500()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder => {
                    webBuilder
                        .UseTestServer()
                        .Configure(app => {

                            app.UseMiddleware<ExceptionHandelingMiddleware>();
                            app.Run((app) => {
                                throw new Exception();
                            });
                        });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync($"api/Address");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
