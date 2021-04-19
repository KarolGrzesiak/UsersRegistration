namespace Domain.Exceptions
{
    public class InvalidPersonalNumberException : DomainException
    {
        public InvalidPersonalNumberException(string value) : base($"{value} is not a valid personal number. It should be in format of \"XXXXX-XXX\", where X is a number.")
        {
        }
    }
}