﻿namespace RedacteurPortaal.DomainModels.Adress;

public class AddressModel : IBaseEntity
{
    public AddressModel()
    {
    }

    public AddressModel(Guid id, string companyName, string address, string postalCode, string phoneNumber, string emailAddress, string webpage)
    {
        this.Id = id;
        this.CompanyName= companyName;
        this.Address = address;
        this.PostalCode = postalCode;
        this.PhoneNumber = phoneNumber;
        this.EmailAddress = emailAddress;
        this.Webpage = webpage;
    }

    public Guid Id { get; set; }

    public string CompanyName { get; private set; }

    public string Address { get; private set; }

    public string PostalCode { get; private set; }

    public string PhoneNumber { get; private set; }

    public string EmailAddress { get; private set; }

    public string Webpage { get; private set; }
}
