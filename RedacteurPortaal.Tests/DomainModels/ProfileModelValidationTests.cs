using FluentValidation;
using RedacteurPortaal.DomainModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedacteurPortaal.Tests.DomainModels;

public class ProfileModelValidationTests
{
    [Fact]
    public void ProfileModelValidatesCorrect()
    {
        var guid = Guid.NewGuid();
        var exc = Record.Exception(() => DomainModelBuilder.CreateProfile());
        Assert.Null(exc);
    }

    [Fact]
    public void ProfileModelThrowsNoFullname()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var profileEmptyContact = new Profile(guid, " ", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
    });
    }    

    [Fact]
    public void ProfileModelCannotValidatesWithEmptyContact()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var profileEmptyContact = new Profile(guid, "Fullname", new ContactDetails(), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [Fact]
    public void ProfileModelThrowsIfPhoneNumberIncorrect()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var wrongPhoneNumber = new Profile(guid, "Fullname", new ContactDetails("email", "number", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.Throws<ValidationException>(() => {
            var emptyPhoneNumber = new Profile(guid, "Fullname", new ContactDetails("email", "", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });

        }

    [Fact]
    public void ProfileModelThrowsIfPostalCodeIncorrect()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var wrongPostalCode = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", "postalcode"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.Throws<ValidationException>(() => {
            var emptyPostalCode = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", ""), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }
    
    [Fact]
    public void ProfileModelThrowsIfEmailEmptyOrWrong()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var wrongEmail = new Profile(guid, "Fullname", new ContactDetails("email", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
        Assert.Throws<ValidationException>(() => {
            var emptyEmail = new Profile(guid, "Fullname", new ContactDetails("", "0612345678", "address", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [Fact]
    public void ProfileModelThrowsIfAddresslEmptyOrWrong()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "", "province", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [Fact]
    public void ProfileModelThrowsIfProvinceEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var emptyProvince = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "", "city", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [Fact]
    public void ProfileModelThrowsIfCityEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "", "1000AB"), "profile", Role.EDITOR, DateTime.UtcNow);
        });
    }

    [Fact]
    public void ProfileModelThrowsIfProfilePhotoEmpty()
    {
        var guid = Guid.NewGuid();
        Assert.Throws<ValidationException>(() => {
            var emptyAddress = new Profile(guid, "Fullname", new ContactDetails("email@email.com", "0612345678", "address", "province", "city", "1000AB"), "", Role.EDITOR, DateTime.UtcNow);
        });
    }
}