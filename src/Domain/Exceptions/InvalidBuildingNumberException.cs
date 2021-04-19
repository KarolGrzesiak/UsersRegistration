namespace Domain.Exceptions
{
    public class InvalidBuildingNumberException : DomainException
    {
        public InvalidBuildingNumberException(string value) : base($"{value} is not a valid building number. Value must be greater than zero")
        {
        }
    }
}