using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels;

namespace RedacteurPortaal.Tests.DomainModels;

[TestClass]
public class AppExceptionTests
{
    [TestMethod]
    public void EmptyCtorBadRequest()
    {
        var exc = new AppException();

        Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
    }

    [TestMethod]
    public void CtorSetsMessage()
    {
        var exc = new AppException("Foo");

        Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
        Assert.AreEqual("Foo", exc.Message);
    }

    [TestMethod]
    public void CtorSetsMessageAndCode()
    {
        var exc = new AppException("Foo", HttpStatusCode.Accepted);

        Assert.AreEqual(HttpStatusCode.Accepted, exc.StatusCode);
        Assert.AreEqual("Foo", exc.Message);
    }
}
