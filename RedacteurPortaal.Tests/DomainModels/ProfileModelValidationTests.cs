using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedacteurPortaal.DomainModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Tests.DomainModels;

[TestClass]
public class ProfileModelValidationTests
{
    [TestMethod]
    public void ProfileModelValidatesCorrect()
    {
        var guid = Guid.NewGuid();
        try
        {
            var exc = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }
    }

    [TestMethod]
    public void ProfileModelThrowsNoFullname()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var profileEmptyContact = new Profile(guid, " ", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelCannotValidatesWithEmptyContact()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var profileEmptyContact = new Profile(guid, "Fullname", new ContactDetails(), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfPhoneNumberIncorrect()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var wrongPhoneNumber = new Profile(guid, "Fullname", new ContactDetails("email", "number", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.ThrowsException<ValidationException>(() => {
            var emptyPhoneNumber = new Profile(guid, "Fullname", new ContactDetails("email", "", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });

    }

    [TestMethod]
    public void ProfileModelThrowsIfPostalCodeIncorrect()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var wrongPostalCode = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", "postalcode"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.ThrowsException<ValidationException>(() => {
            var emptyPostalCode = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", ""), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfEmailEmptyOrWrong()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var wrongEmail = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.ThrowsException<ValidationException>(() => {
            var emptyEmail = new Profile(guid, "Fullname", new ContactDetails("", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfAddresslEmptyOrWrong()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfProvinceEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var emptyProvince = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfCityEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [TestMethod]
    public void ProfileModelThrowsIfProfilePhotoEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.ThrowsException<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "", Role.EDITOR, DateTime.UtcNow);
        });
    }
}