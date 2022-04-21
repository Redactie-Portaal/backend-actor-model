using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RedacteurPortaal.DomainModels.Validation.Profile;

namespace RedacteurPortaal.DomainModels.Profile
{
    public class ContactDetails
    {
        public ContactDetails()
        {
        }

        public ContactDetails(string email, string phoneNumber, string address, string province, string city, string postalCode)
        {
            this.Email = email ?? throw new ArgumentNullException(nameof(email));
            this.PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
            this.Province = province ?? throw new ArgumentNullException(nameof(province));
            this.City = city ?? throw new ArgumentNullException(nameof(city));
            this.PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));

            new ContactDetailsValidator().ValidateAndThrow(this);
        }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Address { get; private set; }

        public string Province { get; private set; }

        public string City { get; private set; }

        public string PostalCode { get; private set; }
    }
}
