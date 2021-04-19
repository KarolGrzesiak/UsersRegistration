using System.IO;

namespace Domain.ValueObjects
{
    public record BasicUserInformation
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Address Address { get; }

        public BasicUserInformation(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public BasicUserInformation ChangeAddress(Address address) =>
            new(FirstName, LastName, address);
        
        public BasicUserInformation Rename(string firstName, string lastName) =>
            new(firstName, lastName, Address);
    }
}