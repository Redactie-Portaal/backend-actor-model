using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.DomainModels.Address
{
    public class IsNullOrWhiteSpaceException : Exception
    {
        public IsNullOrWhiteSpaceException(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Argument must not be the empty string.",
                message);
            }
        }
    }
}