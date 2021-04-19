namespace Domain.Exceptions
{
    public class InvalidZipcodeException : DomainException
    {
        public InvalidZipcodeException(string value) : base($"{value} is not a valid zipcode")
        {
        }
    }
}