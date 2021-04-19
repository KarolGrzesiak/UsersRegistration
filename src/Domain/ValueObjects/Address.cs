using System;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; }
        public uint Number { get; }
        public string ZipCode { get; }
        

        public Address(string street, uint number, string zipCode)
        {
            if (!IsZipCode(zipCode))
                throw new InvalidZipcodeException(zipCode);
            if (number <= 0)
                throw new InvalidBuildingNumberException(number.ToString());
            
            Street = street;
            ZipCode = zipCode;
            Number = number;
        }

        //Just an example, should of course be validated with country consideration
        private bool IsZipCode(string zipCode)
        {
            string pattern = "[0-9]{2}-[0-9]{3}";
            return new Regex(pattern).IsMatch(zipCode);
        }

        public override string ToString()
        {
            return $"{Street} {Number}, {ZipCode}";
        }
    }
}