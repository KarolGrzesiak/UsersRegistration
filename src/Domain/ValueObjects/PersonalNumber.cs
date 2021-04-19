using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public record PersonalNumber
    {
        public string Value { get; }

        public PersonalNumber(string value)
        {
            if (!IsValidPersonalNumber(value))
                throw new InvalidPersonalNumberException(value);
            Value = value;
        }

        private bool IsValidPersonalNumber(string value)
        {
            string pattern = "[0-9]{5}-[0-9]{3}";
            return new Regex(pattern).IsMatch(value);
        }
    }
}