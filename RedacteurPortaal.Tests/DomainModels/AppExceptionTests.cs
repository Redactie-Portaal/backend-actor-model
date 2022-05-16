using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RedacteurPortaal.DomainModels;
using Xunit;

namespace RedacteurPortaal.Tests.DomainModels
{
    public class AppExceptionTests
    {
        [Fact]
        public void EmptyCtorBadRequest()
        {
            var exc = new AppException();

            Assert.Equal(HttpStatusCode.BadRequest, exc.StatusCode);
        }

        [Fact]
        public void CtorSetsMessage()
        {
            var exc = new AppException("Foo");

            Assert.Equal(HttpStatusCode.BadRequest, exc.StatusCode);
            Assert.Equal("Foo", exc.Message);
        }

        [Fact]
        public void CtorSetsMessageAndCode()
        {
            var exc = new AppException("Foo", HttpStatusCode.Accepted);

            Assert.Equal(HttpStatusCode.Accepted, exc.StatusCode);
            Assert.Equal("Foo", exc.Message);
        }
    }
}
